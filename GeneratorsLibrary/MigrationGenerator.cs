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
            {"Organization","info_organization"},
        };
        public string MigrationFileName { get; private set; }
        public string MigrationNamespace { get; set; }
        public string TablePrefix { get; set; }
        /// <summary>
        /// Отображать ли перечисления в байт, по-умолчанию false
        /// </summary>
        public bool MapEnumToIntegerType { get; set; } = false;
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
            StringBuilder sb = new StringBuilder();
            var now = DateTime.Now;
            var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>()
               .Where(cls => cls.Parent is NamespaceDeclarationSyntax); //кроме вложенных классов
            var enums = root.DescendantNodes().OfType<EnumDeclarationSyntax>()
                .Where(en => en.Parent is NamespaceDeclarationSyntax); //кроме вложенных перечислений
            foreach (var cls in classes)
            {
#warning добавляет все классы, а нужно только DomainObject-ы
                KnownTables.Add(cls.Identifier.ToString(),
                    $"{TablePrefix}_{CamelCaseToUnderscore(cls.Identifier.ToString())}");
            }
            string numberOfMigration = $"{now.Year}{now.Month.ToString("D2")}{now.Day.ToString("D2")}{now.Hour.ToString("D2")}{now.Minute.ToString("D2")}";
            MigrationFileName = $"Migration{numberOfMigration}.cs";
            sb.AppendLine($"//Generated by MigrationGenerator, {now}");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using Migrator.Framework;");
            sb.AppendLine();
            sb.AppendLine($"namespace {MigrationNamespace}");
            sb.AppendLine($"{{");
            sb.AppendLine($"\t/// <summary>");
            sb.AppendLine($"\t/// Миграция для создания схемы");
            sb.AppendLine($"\t/// </summary>");
            sb.AppendLine($"\t[Migration({numberOfMigration})]");
            sb.AppendLine($"\tpublic class Migration{numberOfMigration} : Migration");
            sb.AppendLine($"\t{{");
            sb.AppendLine($"\t\tpublic override void Up()");
            sb.AppendLine($"\t\t{{");
            var removeTables = new List<string>();
            var removeFKs = new List<string>();
            var FKs = new List<string>();
            foreach (var cls in classes)
            {
                sb.Append(GenerateTable(enums, classes, cls, "\t\t\t", FKs, removeTables, removeFKs));
            }
            sb.AppendLine();
            foreach (var t in FKs)
            {
                sb.Append("\t\t\t");
                sb.AppendLine(t);
            }
            sb.AppendLine($"\t\t}}");
            sb.AppendLine();
            sb.AppendLine($"\t\tpublic override void Down()");
            sb.AppendLine($"\t\t{{");
            foreach (var r in removeFKs)
            {
                sb.Append($"\t\t\t");
                sb.AppendLine(r);
            }
            sb.AppendLine();
            foreach (var r in removeTables)
            {
                sb.Append($"\t\t\t");
                sb.AppendLine(r);
            }
            sb.AppendLine($"\t\t}}");
            sb.AppendLine($"\t}}");
            sb.AppendLine($"}}");
            return sb.ToString();
        }
        public string GenerateTable(IEnumerable<EnumDeclarationSyntax> enums, IEnumerable<ClassDeclarationSyntax> classes,
            ClassDeclarationSyntax classDecl, string indent, List<string> FKs, List<string> removeTables, List<string> removeFKs)
        {
            StringBuilder sb = new StringBuilder();
            var @namespace = classDecl.Parent as NamespaceDeclarationSyntax;
            var baseList = classDecl.BaseList?.Types.Select(t => GetTypeName(t));
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
#warning не работает, если  ITS.Core.Domain.DomainObject<long>
            {
                sb.Append(indent);
                string tableName = $"{TablePrefix}_{CamelCaseToUnderscore(classDecl.Identifier.ToString())}";
                sb.AppendLine($@"Database.AddTable(""{tableName}"", new[]");
                removeTables.Add($"Database.RemoveTable(\"{tableName}\");");
                sb.Append(indent);
                sb.AppendLine($"\t{{");
                sb.Append(indent);
                sb.AppendLine($"\t\tnew Column(\"id\", DbType.Int64, ColumnProperty.PrimaryKey),");
                int num = 0;
                foreach (var prop in propertiesWithPredefinedTypes)
                {
                    var identifier = prop.Identifier.ToString();
                    var type = GetTypeName(prop.Type);
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
                foreach (var prop in propertiesWithCustomTypes)
                {
                    string identifier = prop.Identifier.ToString();
                    var type = prop.Type.ToString();
                    if (enumNames.Contains(type))
                    {
                        if (MapEnumToIntegerType)
                        {
                            int count = enums.First(n => n.Identifier.ToString() == type).Members.Count;
                            var thisType = enums.First(n => n.Identifier.ToString() == type);
                            var attributes =
                                thisType.AttributeLists.Select(al => al.Attributes).Select(a=>a.ToString()).ToList();
                            bool isFlagsEnum = attributes.Contains("Flags") || attributes.Contains("System.Flags");
                            string typeOfEnum = GetEnumTypeFromCount(count, isFlagsEnum);
                            sb.Append(indent);
                            sb.AppendLine($"\t\tnew Column(\"{CamelCaseToUnderscore(identifier)}\", DbType.{typeOfEnum}, 0),");
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
                            FKs.Add($"Database.AddForeignKey(\"{CamelCaseToUnderscore(classDecl.Identifier.ToString())}_to_{CamelCaseToUnderscore(type)}_{num}\", " +
                                    $"\"{tableName}\", " +
                                    $"\"{CamelCaseToUnderscore(identifier)}_id\", " +
                                    $"\"{KnownTables[type]}\", " +
                                    $"\"id\");");
                            removeFKs.Add($"Database.RemoveForeignKey(\"{tableName}\", \"{CamelCaseToUnderscore(classDecl.Identifier.ToString())}_to_{CamelCaseToUnderscore(type)}_{num}\");");
                            num++;
                        }
                        else if (classes.Select(c=>c.Identifier.ToString()).Contains(type))
                        {
                            FKs.Add($"Database.AddForeignKey(\"{CamelCaseToUnderscore(classDecl.Identifier.ToString())}_to_{CamelCaseToUnderscore(type)}_{num}\"," +
                                   $"\"{tableName}\", " +
                                   $"\"{CamelCaseToUnderscore(identifier)}_id\", " +
                                   $"\"{TablePrefix}_{CamelCaseToUnderscore(type)}\", " +
                                   $" \"id\");");
                            removeFKs.Add($"Database.RemoveForeignKey(\"{tableName}\", \"{CamelCaseToUnderscore(classDecl.Identifier.ToString())}_to_{CamelCaseToUnderscore(type)}_{num}\");");
                            num++;
                        }
                        else
                        {
                            FKs.Add($"//Database.AddForeignKey(\"{tableName}\", \"{CamelCaseToUnderscore(classDecl.Identifier.ToString())}_to_{CamelCaseToUnderscore(type)}_{num}\", " +
                                   $"\"{tableName}\", " +
                                   $"\"{CamelCaseToUnderscore(identifier)}_id\", " +
                                   $"\"{CamelCaseToUnderscore(type)}\", " +
                                   $"\"id\");");
                            removeFKs.Add($"//Database.RemoveForeignKey(\"{tableName}\", \"{CamelCaseToUnderscore(classDecl.Identifier.ToString())}_to_{CamelCaseToUnderscore(type)}_{num}\");");
                            num++;
                        }
                    }
                }
                sb.Append(indent);
                sb.AppendLine($"\t}});");
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
        private static string GetTypeName(TypeSyntax fullType)
        {
            if (fullType is QualifiedNameSyntax qualifiedNameSyntax)
            {
                //if (qualifiedNameSyntax.Right is GenericNameSyntax genericNameSyntax)
                //{
                //    if (genericNameSyntax.TypeArgumentList?.Arguments?.Count > 0)
                //    {
                //        genericNameSyntax.TypeArgumentList.Arguments
                //    }
                //}
                return qualifiedNameSyntax.Right.ToString();
            }
            return fullType.ToString();
        }
        private static string GetTypeName(BaseTypeSyntax fullType)
        {
            //if (fullType is QualifiedNameSyntax qualifiedNameSyntax)
            //{
            //    //if (qualifiedNameSyntax.Right is GenericNameSyntax genericNameSyntax)
            //    //{
            //    //    if (genericNameSyntax.TypeArgumentList?.Arguments?.Count > 0)
            //    //    {
            //    //        genericNameSyntax.TypeArgumentList.Arguments
            //    //    }
            //    //}
            //    return qualifiedNameSyntax.Right.ToString();
            //}
            return fullType.ToString();
        }
        private static string GetEnumTypeFromCount(int count, bool isFlagsEnum=  false)
        {
            if (isFlagsEnum)
            {
                count = (int)Math.Pow(2, count - 2);
            }
            if (count < byte.MaxValue)
            {
                return "Byte";
            }
            else if (count < short.MaxValue)
            {
                return "Int16";
            }
            else if (count < int.MaxValue)
            {
                return "Int32";
            }
            else
            {
                return "Int64";
            }
        }
    }
}
