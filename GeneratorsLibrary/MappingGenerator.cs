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
    //TO DO: сделать поддержку связи многие-ко-многим
    public class MappingGenerator
    {
        private static Dictionary<string, string> PredefinedTypesForMappings = new Dictionary<string, string>()
        {
            {"short","int16"},
            {"int","int"},
            {"long","int64"},
            {"float","single"},
            {"double","double"},
            {"decimal","decimal"},
            {"string","string"},
            {"byte","byte"},
            {"char","char"},
            {"bool","bool"},
            {"Guid","Guid"},
            {"DateTime","date"},
            {"DateTime?","System.Nullable`1[[System.DateTime, mscorlib]], mscorlib"},
            {"Organization","ITS.Core.Domain.Organizations.Organization, ITS.Core"},
        };
        private static Dictionary<string, string> CustomTypesForMappings = new Dictionary<string, string>()
        {
            {"FeatureObject","ITS.Core.Domain.FeatureObjects.FeatureObject, ITS.Core"},
        };
        public List<string> NotMappedPropertyNames { get; private set; } = new List<string>()
        {
            "Photos",
            "PhotoableType",
        };
        public string AssemblyName { get; set; }
        public string TablePrefix { get; set; }
        private CompilationUnitSyntax root;
        private string textForParsing;
        public MappingGenerator(){}
        public MappingGenerator(string assemblyName, string tablePrefix)
        {
            AssemblyName = assemblyName;
            TablePrefix = tablePrefix;
        }
        public MappingGenerator(string assemblyName, string tablePrefix, string[] paths)
        {
            AssemblyName = assemblyName;
            TablePrefix = tablePrefix;
            SetParsedTextFromFiles(paths);
        }
        public MappingGenerator(string assemblyName, string tablePrefix, string code)
        {
            AssemblyName = assemblyName;
            TablePrefix = tablePrefix;
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
                .Where(c => HasSetter(c));
            if ((propertiesWithPredefinedTypes.Count() > 0 || propertiesWithCustomTypes.Count() > 0) &&
                baseList != null && baseList.Contains("DomainObject<long>"))
            {
                stringBuilder.AppendLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                stringBuilder.AppendLine($"<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\">");
                stringBuilder.AppendLine($"\t<class lazy=\"false\" name=\"{@namespace.Name}.{classDecl.Identifier.ToString()}, {AssemblyName}\"" +
                    $" table=\"{TablePrefix}_{CamelCaseToUnderscore(classDecl.Identifier.ToString())}\">");
                stringBuilder.AppendLine($"\t\t<id name=\"ID\" column=\"id\" type=\"long\" unsaved-value=\"0\">");
                stringBuilder.AppendLine($"\t\t\t<generator class=\"hilo\" />");
                stringBuilder.AppendLine($"\t\t</id>");
                foreach (var a in propertiesWithPredefinedTypes)
                {
                    if (PredefinedTypesForMappings.ContainsKey(a.Type.ToString()))
                    {
                        stringBuilder.AppendLine($"\t\t<property column=\"{CamelCaseToUnderscore(a.Identifier.ToString())}\" name=\"{a.Identifier}\" type=\"{PredefinedTypesForMappings[a.Type.ToString()]}\" />");
                    }
                    else
                    {
                        stringBuilder.AppendLine($"\t\t<property column=\"{CamelCaseToUnderscore(a.Identifier.ToString())}\" name=\"{a.Identifier}\" type=\"{a.Type.ToString()}\" />");
                    }
                }
                foreach (var a in propertiesWithCustomTypes)
                {
                    string identifier = a.Identifier.ToString();
                    if (PredefinedTypesForMappings.ContainsKey(a.Type.ToString()))
                    {
                        stringBuilder.AppendLine($"\t\t<property column=\"{CamelCaseToUnderscore(identifier)}\" name=\"{identifier}\" type=\"{PredefinedTypesForMappings[a.Type.ToString()]}\"/>");
                    }
                    else if (CustomTypesForMappings.ContainsKey(a.Type.ToString()))
                    {
                        stringBuilder.AppendLine($"\t\t<many-to-one column=\"{CamelCaseToUnderscore(identifier)}_id\" name=\"{identifier}\" class=\"{CustomTypesForMappings[a.Type.ToString()]}\"/>");
                    }
                    else if (enumNames.Contains(a.Type.ToString()))
                    {
                        var thisType = enums.First(n => n.Identifier.ToString() == a.Type.ToString());
                        var enumNamespace = (thisType.Parent as NamespaceDeclarationSyntax).Name.ToString();
                        stringBuilder.AppendLine($"\t\t<property column=\"{CamelCaseToUnderscore(identifier)}\" name=\"{identifier}\" type=\"NHibernate.Type.EnumStringType`1[[{enumNamespace}.{a.Type.ToString()}, {AssemblyName}]], NHibernate\" not-null=\"true\"/>");
                    }
                    else
                    {
                        stringBuilder.AppendLine($"\t\t<!--many-to-one column=\"{CamelCaseToUnderscore(identifier)}_id\" name=\"{identifier}\" class=\"{@namespace.Name}.{a.Type.ToString()}\"/-->");
                    }
                }
                stringBuilder.AppendLine($"\t</class>");
                stringBuilder.AppendLine($"</hibernate-mapping>");
                return new KeyValuePair<string, string>(classDecl.Identifier.ToString() + ".hbm.xml", stringBuilder.ToString());
            }
            return null;
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
