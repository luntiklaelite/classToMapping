using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneratorsLibrary.StringUtilities;

namespace GeneratorsLibrary
{
    public class MigrationGenerator
    {
        public static Dictionary<string, string> TypesForMigrations = new Dictionary<string, string>()
        {
            {"float[]","Binary"},

            {"Int16","Int16"},
            {"short","Int16"},
            {"short?","Int16"},
            {"Int16?","Int16"},
            {"Nullable<Int16>","Int16"},
            {"Nullable<short>","Int16"},

            {"Int32","Int32"},
            {"int","Int32"},
            {"int?","Int32"},
            {"Int32?","Int32"},
            {"Nullable<int>","Int32"},
            {"Nullable<Int32>","Int32"},

            {"Int64","Int64"},
            {"long","Int64"},
            {"long?","Int64"},
            {"Int64?","Int64"},
            {"Nullable<long>","Int64"},
            {"Nullable<Int64>","Int64"},

            {"Single","Single"},
            {"float","Single"},
            {"float?","Single"},
            {"Single?","Single"},
            {"Nullable<float>","Single"},
            {"Nullable<Single>","Single"},

            {"Double","Double"},
            {"double","Double"},
            {"double?","Double"},
            {"Double?","Double"},
            {"Nullable<double>","Double"},
            {"Nullable<Double>","Double"},

            {"Decimal","Decimal"},
            {"decimal","Decimal"},
            {"decimal?","Decimal"},
            {"Decimal?","Decimal"},
            {"Nullable<decimal>","Decimal"},
            {"Nullable<Decimal>","Decimal"},

            {"string","String"},
            {"String","String"},

            {"byte","Byte"},
            {"Byte","Byte"},
            {"byte?","Byte"},
            {"Byte?","Byte"},
            {"Nullable<byte>","Byte"},
            {"Nullable<Byte>","Byte"},

            {"char","StringFixedLength"},
            {"Char","StringFixedLength"},
            {"char?","StringFixedLength"},
            {"Char?","StringFixedLength"},
            {"Nullable<char>","StringFixedLength"},
            {"Nullable<Char>","StringFixedLength"},

            {"bool","Boolean"},
            {"Boolean","Boolean"},
            {"bool?","Boolean"},
            {"Boolean?","Boolean"},
            {"Nullable<bool>","Boolean"},
            {"Nullable<Boolean>","Boolean"},

            {"Guid","Guid"},
            {"Guid?","Guid"},
            {"Nullable<Guid>","Guid"},

            {"DateTime","Date"},
            {"DateTime?","Date"},
            {"Nullable<DateTime>","Date"},
        };
        public List<string> NotMappedPropertyNames { get; private set; } = new List<string>()
        {
            "Photos",
            "PhotoableType",
        };
        public static Dictionary<string, string> KnownTables = new Dictionary<string, string>()
        {
            {"FeatureObject","featureobject"},
            {"Organization","organization"},
        };
        public string MigrationFileName { get; private set; }
        public string MigrationNamespace { get; set; }
        public string TablePrefix { get; set; }
        /// <summary>
        /// Отображать ли перечисления в байт, по-умолчанию false
        /// </summary>
        public bool MapEnumToByte { get; set; } = false;
        private CompilationUnitSyntax root;
        private string textForParsing;

        public MigrationGenerator(string migrationNamespace, string tablePrefix, string[] paths) 
        {
            MigrationNamespace = migrationNamespace ?? throw new ArgumentNullException(nameof(migrationNamespace));
            TablePrefix = tablePrefix ?? throw new ArgumentNullException(nameof(tablePrefix));
            textForParsing = string.Empty;
            foreach (var path in paths)
            {
                textForParsing += File.ReadAllText(path);
            }
            SyntaxTree tree = CSharpSyntaxTree.ParseText(textForParsing);
            root = tree.GetCompilationUnitRoot();
            
        }
        public MigrationGenerator()
        {

        }
        public void SetParsedTextFromFiles(string[] paths)
        {
            textForParsing = string.Empty;
            foreach (var path in paths)
            {
                textForParsing += File.ReadAllText(path);
            }
            SyntaxTree tree = CSharpSyntaxTree.ParseText(textForParsing);
            root = tree.GetCompilationUnitRoot();
        }
        public void SetParsedTextFromFiles(string path)
        {
            textForParsing = File.ReadAllText(path);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(textForParsing);
            root = tree.GetCompilationUnitRoot();
        }
        public void SetParsedTextFromCode(string csCode)
        {
            textForParsing = csCode;
            SyntaxTree tree = CSharpSyntaxTree.ParseText(textForParsing);
            root = tree.GetCompilationUnitRoot();
        }
        public string GenerateMigration()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var now = DateTime.Now;
            var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>()
               .Where(cls => cls.Parent is NamespaceDeclarationSyntax); //кроме вложенных классов
            var enums = root.DescendantNodes().OfType<EnumDeclarationSyntax>()
                .Where(en => en.Parent is NamespaceDeclarationSyntax); //кроме вложенных перечислений
            foreach (var cls in classes)
            {
                KnownTables.Add(cls.Identifier.ToString(),
                    $"{TablePrefix}_{CamelCaseToUnderscore(cls.Identifier.ToString())}");
            }
            string numberOfMigration = $"{now.Year}{now.Month.ToString("D2")}{now.Day.ToString("D2")}{now.Hour.ToString("D2")}{now.Minute.ToString("D2")}";
            MigrationFileName = $"Migration{numberOfMigration}.cs";
            stringBuilder.AppendLine($"//Generated by MigrationGenerator, {now}");
            stringBuilder.AppendLine("using System.Data;");
            stringBuilder.AppendLine("using Migrator.Framework;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"namespace {MigrationNamespace}");
            stringBuilder.AppendLine($"{{");
            stringBuilder.AppendLine($"\t/// <summary>");
            stringBuilder.AppendLine($"\t/// Миграция для создания схемы");
            stringBuilder.AppendLine($"\t/// </summary>");
            stringBuilder.AppendLine($"\t[Migration({numberOfMigration})]");
            stringBuilder.AppendLine($"\tpublic class Migration{numberOfMigration} : Migration");
            stringBuilder.AppendLine($"\t{{");
            stringBuilder.AppendLine($"\t\tpublic override void Up()");
            stringBuilder.AppendLine($"\t\t{{");
            var tableNames = new List<string>();
            foreach (var cls in classes)
            {
                tableNames.Add($"{TablePrefix}_{cls.Identifier.ToString()}");
                stringBuilder.Append(GenerateTable(enums, classes, cls, "\t\t\t"));
            }
            stringBuilder.AppendLine($"\t\t}}");
            stringBuilder.AppendLine($"\t\tpublic override void Down()");
            stringBuilder.AppendLine($"\t\t{{");
            foreach (var tn in tableNames)
            {
                stringBuilder.AppendLine($"\t\t\tDatabase.RemoveTable(\"{tn}\");");
            }
            stringBuilder.AppendLine($"\t\t}}");
            stringBuilder.AppendLine($"\t}}");
            stringBuilder.AppendLine($"}}");
            return stringBuilder.ToString();
        }
        public string GenerateTable(IEnumerable<EnumDeclarationSyntax> enums, IEnumerable<ClassDeclarationSyntax> classes,
            ClassDeclarationSyntax classDecl, string indent)
        {
            StringBuilder sb = new StringBuilder();
            var @namespace = classDecl.Parent as NamespaceDeclarationSyntax;
            var baseList = classDecl.BaseList?.Types.Select(t => t.ToString());
            var enumNames = enums.Select(e => e.Identifier.ToString()).ToList();
            var propertiesWithPredefinedTypes = classDecl.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(c => c.Parent is ClassDeclarationSyntax)
                .Where(c => c.DescendantNodes().OfType<PredefinedTypeSyntax>().Count() > 0)
                .Where(c => !NotMappedPropertyNames.Contains(c.Identifier.ToString()))
                .Where(c => HasSetter(c));
            var propertiesWithCustomTypes = classDecl.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(c => c.Parent is ClassDeclarationSyntax)
                .Where(c => c.DescendantNodes().OfType<IdentifierNameSyntax>().Count() > 0)
                .Where(c => !NotMappedPropertyNames.Contains(c.Identifier.ToString()))
                .Where(c => HasSetter(c));
            if ((propertiesWithPredefinedTypes.Count() > 0 || propertiesWithCustomTypes.Count() > 0) &&
                baseList != null && baseList.Contains("DomainObject<long>"))
            {
                sb.Append(indent);
                string tableName = $"{TablePrefix}_{CamelCaseToUnderscore(classDecl.Identifier.ToString())}";
                sb.AppendLine($@"Database.AddTable(""{tableName}"", new[]");
                sb.Append(indent);
                sb.AppendLine($"\t{{");
                sb.Append(indent);
                sb.AppendLine($"\t\tnew Column(\"id\", DbType.Int64, ColumnProperty.PrimaryKey),");
                int num = 0;
                foreach (var prop in propertiesWithPredefinedTypes)
                {
                    var identifier = prop.Identifier.ToString();
                    var type = prop.Type.ToString();
                    if (TypesForMigrations.ContainsKey(type))
                    {
                        sb.Append(indent);
                        sb.AppendLine($"\t\tnew Column(\"{CamelCaseToUnderscore(identifier)}\", DbType.{TypesForMigrations[type]}),");
                    }
                    else
                    {
                        sb.Append(indent);
                        sb.AppendLine($"\t\t//new Column(\"{CamelCaseToUnderscore(identifier)}\", DbType.Binary),");
                    }
                }
                List<string> types = new List<string>();
                foreach (var prop in propertiesWithCustomTypes)
                {
                    string identifier = prop.Identifier.ToString();
                    var type = prop.Type.ToString();
                    if (enumNames.Contains(type))
                    {
                        if (MapEnumToByte)
                        {
                            sb.Append(indent);
                            sb.AppendLine($"\t\tnew Column(\"{CamelCaseToUnderscore(identifier)}\", DbType.Byte, 0),");
                        }
                        else
                        {
                            var thisType = enums.First(n => n.Identifier.ToString() == type);
                            var members = thisType.DescendantNodes().OfType<EnumMemberDeclarationSyntax>();
                            sb.Append(indent);
                            sb.AppendLine($"\t\tnew Column(\"{CamelCaseToUnderscore(identifier)}\", DbType.String, \"{members.First().Identifier.ToString()}\"),");
                        }
                    }
                    else if(TypesForMigrations.ContainsKey(type))
                    {
                        sb.Append(indent);
                        sb.AppendLine($"\t\tnew Column(\"{CamelCaseToUnderscore(identifier)}\", DbType.{TypesForMigrations[type]}),");
                    }
                    else
                    {
                        sb.Append(indent);
                        sb.AppendLine($"\t\tnew Column(\"{CamelCaseToUnderscore(identifier)}_id\", DbType.Int64),");
                        //генерация внешнего ключа
                        if (KnownTables.ContainsKey(type))
                        {
                            types.Add($"Database.AddForeignKey(\"{CamelCaseToUnderscore(classDecl.Identifier.ToString())}_to_{CamelCaseToUnderscore(type)}_{num}\"," +
                                    $" \"{tableName}\", " +
                                    $"\"{CamelCaseToUnderscore(identifier)}_id\", " +
                                    $"\"{KnownTables[type]}\"," +
                                    $" \"id\");");
                            num++;
                        }
                        else if (classes.Select(c=>c.Identifier.ToString()).Contains(type))
                        {
                            types.Add($"Database.AddForeignKey(\"{CamelCaseToUnderscore(classDecl.Identifier.ToString())}_to_{CamelCaseToUnderscore(type)}_{num}\"," +
                                   $" \"{tableName}\", " +
                                   $"\"{CamelCaseToUnderscore(identifier)}_id\", " +
                                   $"\"{TablePrefix}_{CamelCaseToUnderscore(type)}\"," +
                                   $" \"id\");");
                            num++;
                        }
                        else
                        {
                            types.Add($"//Database.AddForeignKey(\"{CamelCaseToUnderscore(classDecl.Identifier.ToString())}_to_{CamelCaseToUnderscore(type)}_{num}\"," +
                                   $" \"{tableName}\", " +
                                   $"\"{CamelCaseToUnderscore(identifier)}_id\", " +
                                   $"\"{CamelCaseToUnderscore(type)}\"," +
                                   $" \"id\");");
                            num++;
                        }
                    }
                }
                sb.Append(indent);
                sb.AppendLine($"\t}});");
                foreach (var t in types)
                {
                    sb.Append(indent);
                    sb.AppendLine(t);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// Возвращает true, если свойство имеет сеттер
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static bool HasSetter(PropertyDeclarationSyntax c)
        {
            return c.AccessorList != null &&
                (c.AccessorList.ChildNodes().Select(prop => prop.ToString()).Contains("set;") ||
                c.AccessorList.ChildNodes().OfType<AccessorDeclarationSyntax>().Count() > 1);
        }
    }
}
