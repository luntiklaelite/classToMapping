using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static classToMapping.Models.StringUtilities;


namespace classToMapping.Models
{
    //TO DO: сделать поддержку связи многие-ко-многим
    public class MappingGenerator
    {
        public static Dictionary<string, string> TypesForMappings = new Dictionary<string, string>()
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
            {"FeatureObject","ITS.Core.Domain.FeatureObjects.FeatureObject, ITS.Core"},
        };
        public static Dictionary<string, string> TypesForMigrations = new Dictionary<string, string>()
        {
            {"short","Int16"},
            {"int","Int32"},
            {"long","Int64"},
            {"float","Single"},
            {"double","Double"},
            {"decimal","Decimal"},
            {"string","String"},
            {"byte","Byte"},
            {"char","StringFixedLength"},
            {"bool","Boolean"},
            {"Guid","Guid"},
            {"DateTime","Date"},
            {"DateTime?","Date"},
        };
        public List<string> NotMappedPropertyNames { get; private set; } = new List<string>()
        {
            "Photos",
            "PhotoableType",
        };
        public string AssemblyName { get; set; }
        public string FileName { get;private set; }
        private string textForParsing;
        public MappingGenerator()
        {

        }
        public MappingGenerator(string assemblyName)
        {
            AssemblyName = assemblyName;
        }
        public void SetParsedTextFromFiles(string[] paths)
        {
            textForParsing = string.Empty;
            foreach (var path in paths)
            {
                textForParsing += File.ReadAllText(path);
            }
        }
        public void SetParsedTextFromFiles(string path)
        {
            textForParsing = File.ReadAllText(path);
        }
        public void SetParsedTextFromCode(string csCode)
        {
            textForParsing = csCode;
        }
        public string GenerateMapping()
        {
            StringBuilder stringBuilder = new StringBuilder();
            SyntaxTree tree = CSharpSyntaxTree.ParseText(textForParsing);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            var mainNamespace = root.ChildNodes().OfType<NamespaceDeclarationSyntax>().First();
            var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
            var enums = root.DescendantNodes().OfType<EnumDeclarationSyntax>();
            var enumNames = enums.Select(e => e.Identifier.ToString()).ToList();
            var mainClass = classes.First();
            var baseList = mainClass.BaseList?.Types.Select(t => t.ToString());

            FileName = mainClass.Identifier.ToString() + ".hbm.xml";

            var propertiesWithPredefinedTypes = root.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(c => c.Parent is ClassDeclarationSyntax)
                .Where(c => c.DescendantNodes().OfType<PredefinedTypeSyntax>().Count() > 0)
                .Where(c => HasSetter(c)); //нужны только свойства, содержащие сеттер
            var propertiesWithCustomTypes = root.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(c => c.Parent is ClassDeclarationSyntax)
                .Where(c => c.DescendantNodes().OfType<IdentifierNameSyntax>().Count() > 0)
                .Where(c => !NotMappedPropertyNames.Contains(c.Identifier.ToString()))
                .Where(c => HasSetter(c)); //нужны только свойства, содержащие сеттер
            stringBuilder.AppendLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            stringBuilder.AppendLine($"<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\">");
            stringBuilder.AppendLine($"\t<class lazy=\"false\" name=\"{mainNamespace.Name}.{mainClass.Identifier.ToString()}, {AssemblyName}\" table=\"{AssemblyName}_{CamelCaseToUnderscore(mainClass.Identifier.ToString())}\">");
            if (baseList != null && baseList.Contains("DomainObject<long>"))
            {
                stringBuilder.AppendLine($"\t\t<id name=\"ID\" column=\"id\" type=\"long\" unsaved-value=\"0\">");
                stringBuilder.AppendLine($"\t\t\t<generator class=\"hilo\" />");
                stringBuilder.AppendLine($"\t\t</id>");
            }
            foreach (var a in propertiesWithPredefinedTypes)
            {
                if (TypesForMappings.ContainsKey(a.Type.ToString()))
                {
                    stringBuilder.AppendLine($"\t\t<property column=\"{CamelCaseToUnderscore(a.Identifier.ToString())}\" name=\"{a.Identifier}\" type=\"{TypesForMappings[a.Type.ToString()]}\" />");
                }
                else
                {
                    stringBuilder.AppendLine($"\t\t<property column=\"{CamelCaseToUnderscore(a.Identifier.ToString())}\" name=\"{a.Identifier}\" type=\"{a.Type.ToString()}\" />");
                }
            }
            foreach (var a in propertiesWithCustomTypes)
            {
                string identifier = a.Identifier.ToString();
                if (TypesForMappings.ContainsKey(a.Type.ToString()))
                {
                    stringBuilder.AppendLine($"\t\t<many-to-one column=\"{CamelCaseToUnderscore(identifier)}\" name=\"{identifier}\" class=\"{TypesForMappings[a.Type.ToString()]}\"/>");
                }
                else if(enumNames.Contains(a.Type.ToString()))
                {
                    var thisType = enums.First(n => n.Identifier.ToString() == a.Type.ToString());
                    var enumNamespace = (thisType.Parent as NamespaceDeclarationSyntax).Name.ToString();
                    stringBuilder.AppendLine($"\t\t<property column=\"{CamelCaseToUnderscore(identifier)}\" name=\"{identifier}\" type=\"NHibernate.Type.EnumStringType`1[[{enumNamespace}.{identifier}, {AssemblyName}]], NHibernate\" not-null=\"true\"/>");
                }
                else
                {
                    stringBuilder.AppendLine($"\t\t<many-to-one column=\"{CamelCaseToUnderscore(identifier)}_id\" name=\"{identifier}\" class=\"{mainNamespace.Name}.{a.Type.ToString()}\"/>");
                }
            }
            stringBuilder.AppendLine($"\t</class>");
            stringBuilder.AppendLine($"</hibernate-mapping>");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Возвращает true, если свойство имеет сеттер
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static bool HasSetter(PropertyDeclarationSyntax c)
        {
#warning может не работать с не авто свойствами 
            return c.AccessorList != null &&
                                c.AccessorList.ChildNodes().Select(prop => prop.ToString()).Contains("set;");
        }

        //public static string GetTableNamePrefix(string assemblyName)
        //{
        //    assemblyName.
        //    return;
        //}
    }
}
