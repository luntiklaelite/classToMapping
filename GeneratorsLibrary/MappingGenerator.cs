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
    //todo: сделать поддержку связи многие-ко-многим и один-ко-многим
    public class MappingGenerator
    {
        private static Dictionary<string, string> PredefinedTypesForMappings = new Dictionary<string, string>()
        {
            {"float[]", "Serializable"},
            {"byte[]", "System.Byte[], mscorlib"},

            {"Int16","short"},
            {"short","short"},
            {"short?","System.Nullable`1[[System.Int16, mscorlib]], mscorlib"},
            {"Int16?","System.Nullable`1[[System.Int16, mscorlib]], mscorlib"},
            {"Nullable<Int16>","System.Nullable`1[[System.Int16, mscorlib]], mscorlib"},
            {"Nullable<short>","System.Nullable`1[[System.Int16, mscorlib]], mscorlib"},

            {"Int32","int"},
            {"int","int"},
            {"int?","System.Nullable`1[[System.Int32, mscorlib]], mscorlib"},
            {"Int32?","System.Nullable`1[[System.Int32, mscorlib]], mscorlib"},
            {"Nullable<int>","System.Nullable`1[[System.Int32, mscorlib]], mscorlib"},
            {"Nullable<Int32>","System.Nullable`1[[System.Int32, mscorlib]], mscorlib"},

            {"Int64","long"},
            {"long","long"},
            {"long?","System.Nullable`1[[System.Int64, mscorlib]], mscorlib"},
            {"Int64?","System.Nullable`1[[System.Int64, mscorlib]], mscorlib"},
            {"Nullable<long>","System.Nullable`1[[System.Int64, mscorlib]], mscorlib"},
            {"Nullable<Int64>","System.Nullable`1[[System.Int64, mscorlib]], mscorlib"},

            {"Single","single"},
            {"float","single"},
            {"float?","System.Nullable`1[[System.Single, mscorlib]], mscorlib"},
            {"Single?","System.Nullable`1[[System.Single, mscorlib]], mscorlib"},
            {"Nullable<float>","System.Nullable`1[[System.Single, mscorlib]], mscorlib"},
            {"Nullable<Single>","System.Nullable`1[[System.Single, mscorlib]], mscorlib"},

            {"Double","double"},
            {"double","double"},
            {"double?","System.Nullable`1[[System.Double, mscorlib]], mscorlib"},
            {"Double?","System.Nullable`1[[System.Double, mscorlib]], mscorlib"},
            {"Nullable<double>","System.Nullable`1[[System.Double, mscorlib]], mscorlib"},
            {"Nullable<Double>","System.Nullable`1[[System.Double, mscorlib]], mscorlib"},

            {"Decimal","decimal"},
            {"decimal","decimal"},
            {"decimal?","System.Nullable`1[[System.Decimal, mscorlib]], mscorlib"},
            {"Decimal?","System.Nullable`1[[System.Decimal, mscorlib]], mscorlib"},
            {"Nullable<decimal>","System.Nullable`1[[System.Decimal, mscorlib]], mscorlib"},
            {"Nullable<Decimal>","System.Nullable`1[[System.Decimal, mscorlib]], mscorlib"},

            {"string","string"},
            {"String","string"},

            {"byte","byte"},
            {"Byte","byte"},
            {"byte?","System.Nullable`1[[System.Byte, mscorlib]], mscorlib"},
            {"Byte?","System.Nullable`1[[System.Byte, mscorlib]], mscorlib"},
            {"Nullable<byte>","System.Nullable`1[[System.Byte, mscorlib]], mscorlib"},
            {"Nullable<Byte>","System.Nullable`1[[System.Byte, mscorlib]], mscorlib"},

            {"char","char"},
            {"Char","char"},
            {"char?","System.Nullable`1[[System.Char, mscorlib]], mscorlib"},
            {"Char?","System.Nullable`1[[System.Char, mscorlib]], mscorlib"},
            {"Nullable<char>","System.Nullable`1[[System.Char, mscorlib]], mscorlib"},
            {"Nullable<Char>","System.Nullable`1[[System.Char, mscorlib]], mscorlib"},

            {"bool","bool"},
            {"Boolean","bool"},
            {"bool?","System.Nullable`1[[System.Boolean, mscorlib]], mscorlib"},
            {"Boolean?","System.Nullable`1[[System.Boolean, mscorlib]], mscorlib"},
            {"Nullable<bool>","System.Nullable`1[[System.Boolean, mscorlib]], mscorlib"},
            {"Nullable<Boolean>","System.Nullable`1[[System.Boolean, mscorlib]], mscorlib"},

            {"Guid","Guid"},
            {"Guid?","System.Nullable`1[[System.Guid, mscorlib]], mscorlib"},
            {"Nullable<Guid>","System.Nullable`1[[System.Guid, mscorlib]], mscorlib"},

            {"DateTime","date"},
            {"DateTime?","System.Nullable`1[[System.DateTime, mscorlib]], mscorlib"},
            {"Nullable<DateTime>","System.Nullable`1[[System.DateTime, mscorlib]], mscorlib"},
        };
        private static Dictionary<string, string> CustomTypesForMappings = new Dictionary<string, string>()
        {
            {"FeatureObject","ITS.Core.Domain.FeatureObjects.FeatureObject, ITS.Core"},
            {"Organization","ITS.Core.Domain.Organizations.Organization, ITS.Core"},
        };
        private CompilationUnitSyntax root;
        private string textForParsing;

        public List<string> NotMappedPropertyNames { get; set; } = new List<string>()
        {
            "Photos",
            "PhotoableType",
        };
        public List<string> AlreadyExistsClassNames { get; set; } = new List<string>()
        {
            "Material",
            "Defect"
        };
        public List<string> PropertyWithCascadeAll { get; } = new List<string>();
        public string CustomXmlInMainMapping { get; set; }
        public string AssemblyName { get; set; }
        public string TablePrefix { get; set; }
        /// <summary>
        /// Отображать ли перечисления в байт, по-умолчанию false
        /// </summary>
        public bool MapEnumToIntegerType { get; set; } = false;

        public MappingGenerator(){}
        public MappingGenerator(string assemblyName, string tablePrefix)
        {
            AssemblyName = assemblyName;
            TablePrefix = tablePrefix;
        }
        public MappingGenerator(string assemblyName, string tablePrefix, string[] paths)
            :this(assemblyName, tablePrefix)
        {
            SetParsedTextFromFiles(paths);
        }
        public MappingGenerator(string assemblyName, string tablePrefix, string code)
            : this(assemblyName, tablePrefix)
        {
            SetParsedTextFromCode(code);
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
        public List<KeyValuePair<string, string>> GenerateMappings()
        {
            if (root == null)
            {
                return null;
            }
            var result = new List<KeyValuePair<string, string>>();
            var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>()
                .Where(cls => cls.Parent is NamespaceDeclarationSyntax); //кроме вложенных классов
            var enums = root.DescendantNodes().OfType<EnumDeclarationSyntax>()
                .Where(en => en.Parent is NamespaceDeclarationSyntax); //кроме вложенных перечислений
            foreach (var cls in classes)
            {
                if (!CustomTypesForMappings.ContainsKey(cls.Identifier.ToString()))
                {
                    CustomTypesForMappings.Add(cls.Identifier.ToString(),
                    $"{(cls.Parent as NamespaceDeclarationSyntax).Name}.{cls.Identifier.ToString()}, {AssemblyName}");
                }
            }
            foreach (var cls in classes)
            {
                var kvp = GenerateMapping(cls, enums);
                if (kvp != null)
                {
                    result.Add(kvp.Value);
                }
            }
            return result;
        }
        private KeyValuePair<string, string>? GenerateMapping(ClassDeclarationSyntax classDecl, IEnumerable<EnumDeclarationSyntax> enums)
        {
            StringBuilder stringBuilder = new StringBuilder();
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
                .Where(c => HasSetter(c))
                .Where(c => !propertiesWithPredefinedTypes.Contains(c));
            if ((propertiesWithPredefinedTypes.Count() > 0 || propertiesWithCustomTypes.Count() > 0) &&
                baseList != null && baseList.Contains("DomainObject<long>"))
            {
                stringBuilder.AppendLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                stringBuilder.AppendLine($"<!--Generated:{DateTime.Now}-->");
                if (AlreadyExistsClassNames.Contains(classDecl.Identifier.ToString()))
                {
                    stringBuilder.AppendLine($"<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" auto-import=\"false\">");
                }
                else
                {
                    stringBuilder.AppendLine($"<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\">");
                }
                stringBuilder.AppendLine($"\t<class lazy=\"false\" name=\"{@namespace.Name}.{classDecl.Identifier.ToString()}, {AssemblyName}\"" +
                    $" table=\"{TablePrefix}_{CamelCaseToUnderscore(classDecl.Identifier.ToString())}\">");
                stringBuilder.AppendLine($"\t\t<id name=\"ID\" column=\"id\" type=\"long\" unsaved-value=\"0\">");
                stringBuilder.AppendLine($"\t\t\t<generator class=\"hilo\" />");
                stringBuilder.AppendLine($"\t\t</id>");
                foreach (var prop in propertiesWithPredefinedTypes)
                {
                    string line = GetPredefinedPropLine(prop);
                    if (!string.IsNullOrEmpty(line))
                    {
                        stringBuilder.AppendLine(line);
                    }
                }
                foreach (var prop in propertiesWithCustomTypes)
                {
                    string identifier = prop.Identifier.ToString();
                    string type = GetTypeName(prop.Type);
                    string line = GetCustomPropLine(enums, @namespace, enumNames, identifier, type);
                    if (!string.IsNullOrEmpty(line))
                    {
                        stringBuilder.AppendLine(line);
                    }
                }
                //todo: работает только в частном случае
                if ($"{ TablePrefix}_{ CamelCaseToUnderscore(classDecl.Identifier.ToString())}" 
                    == "bridges_bridge")
                {
                    stringBuilder.AppendLine(CustomXmlInMainMapping);
                }
                stringBuilder.AppendLine($"\t</class>");
                stringBuilder.AppendLine($"</hibernate-mapping>");
                return new KeyValuePair<string, string>(classDecl.Identifier.ToString() + ".hbm.xml", stringBuilder.ToString());
            }
            return null;
        }

        public string GetPredefinedPropLine(PropertyDeclarationSyntax prop)
        {
            if (PredefinedTypesForMappings.ContainsKey(prop.Type.ToString()))
            {
                return $"\t\t<property column=\"{CamelCaseToUnderscore(prop.Identifier.ToString())}\" name=\"{prop.Identifier}\" type=\"{PredefinedTypesForMappings[prop.Type.ToString()]}\" />";
            }
            else
            {
                return  $"\t\t<!--property column=\"{CamelCaseToUnderscore(prop.Identifier.ToString())}\" name=\"{prop.Identifier}\" type=\"{prop.Type.ToString()}\" /-->";
            }
        }
        public string GetCustomPropLine(IEnumerable<EnumDeclarationSyntax> enums, NamespaceDeclarationSyntax @namespace, List<string> enumNames, string identifier, string type)
        {
            if (PredefinedTypesForMappings.ContainsKey(type))
            {
                return $"\t\t<property column=\"{CamelCaseToUnderscore(identifier)}\" name=\"{identifier}\" type=\"{PredefinedTypesForMappings[type]}\"/>";
            }
            else if (CustomTypesForMappings.ContainsKey(type))
            {
                if (type == "FeatureObject")
                {
                    return $"\t\t<many-to-one column=\"{CamelCaseToUnderscore(identifier)}_id\" name=\"{identifier}\" class=\"{CustomTypesForMappings[type]}\" cascade=\"all\" not-null=\"true\"/>";
                }
                else if(PropertyWithCascadeAll.Contains(identifier))
                {
                    return $"\t\t<many-to-one column=\"{CamelCaseToUnderscore(identifier)}_id\" name=\"{identifier}\" class=\"{CustomTypesForMappings[type]}\" cascade=\"all\"/>";
                }
                return $"\t\t<many-to-one column=\"{CamelCaseToUnderscore(identifier)}_id\" name=\"{identifier}\" class=\"{CustomTypesForMappings[type]}\"/>";
            }
            else if (enumNames.Contains(type))
            {
                var thisType = enums.First(n => n.Identifier.ToString() == type);
                var enumNamespace = (thisType.Parent as NamespaceDeclarationSyntax).Name.ToString();
                if (!MapEnumToIntegerType)
                {
                    return $"\t\t<property column=\"{CamelCaseToUnderscore(identifier)}\" name=\"{identifier}\" type=\"NHibernate.Type.EnumStringType`1[[{enumNamespace}.{type}, {AssemblyName}]], NHibernate\" not-null=\"true\"/>";
                }
                return $"\t\t<property column=\"{CamelCaseToUnderscore(identifier)}\" name=\"{identifier}\" type=\"{enumNamespace}.{type}, {AssemblyName}\" not-null=\"true\"/>";
            }
            else
            {
                // добавить поддержку IList<>
                //return $"\t\t<!--many-to-one column=\"{CamelCaseToUnderscore(identifier)}_id\" name=\"{identifier}\" class=\"{@namespace.Name}.{type}, {AssemblyName}\"/-->";
                return $"";
            }
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
    }
}
