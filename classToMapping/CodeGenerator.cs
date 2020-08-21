using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace classToMapping
{
    //TO DO: сделать поддержку enum
    public class CodeGenerator
    {
        public static Dictionary<string, string> TypesForMappings = new Dictionary<string, string>()
        {
            {"float","single"},
            {"string","string"},
            {"int","int"},
            {"double","double"},
            {"decimal","decimal"},
            {"DateTime","date"},
            {"DateTime?","System.Nullable`1[[System.DateTime, mscorlib]], mscorlib"},
            {"Organization","ITS.Core.Domain.Organizations.Organization, ITS.Core"},
            {"FeatureObject","ITS.Core.Domain.FeatureObjects.FeatureObject, ITS.Core"},
        };
        public string AssemblyName { get; set; }
        public string FileName { get; set; }
        private string textForParsing;
        public CodeGenerator()
        {

        }
        public CodeGenerator(string assemblyName)
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
            var nameSpace = root.ChildNodes().OfType<NamespaceDeclarationSyntax>().First();
            var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
            var mainClass = classes.First();
            string name = mainClass.Identifier.ToString();
            //var props = mainClass.ChildNodes().OfType<PropertyDeclarationSyntax>();
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
                .Where(c => c.Identifier.ToString() != "Photos" && c.Identifier.ToString() != "PhotoableType") //свойства, связанные с фото в маппинг не записываются
                .Where(c => HasSetter(c)); //нужны только свойства, содержащие сеттер
            stringBuilder.AppendLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            stringBuilder.AppendLine($"<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\">");
            stringBuilder.AppendLine($"\t<class lazy=\"false\" name=\"{nameSpace.Name}.{name}, {AssemblyName}\" table=\"{AssemblyName}_{CamelCaseToUnderscore(name)}\">");
            if (baseList.Contains("DomainObject<long>"))
            {
                stringBuilder.AppendLine($"\t\t<id name=\"ID\" column=\"id\" type=\"long\" unsaved-value=\"0\">");
                stringBuilder.AppendLine($"\t\t\t<generator class=\"hilo\" />");
                stringBuilder.AppendLine($"\t\t</id>");
            }
            foreach (var a in propertiesWithCustomTypes)
            {
                if (TypesForMappings.ContainsKey(a.Type.ToString()))
                {
                    stringBuilder.AppendLine($"\t\t<many-to-one column=\"{CamelCaseToUnderscore(a.Identifier.ToString())}\" name=\"{a.Identifier}\" class=\"{TypesForMappings[a.Type.ToString()]}\"/>");
                }
                else
                {
                    stringBuilder.AppendLine($"\t\t<many-to-one column=\"{CamelCaseToUnderscore(a.Identifier.ToString())}_id\" name=\"{a.Identifier}\" class=\"{a.Type.ToString()}\"/>");
                }
            }
            foreach (var a in propertiesWithPredefinedTypes)
            {
                if (TypesForMappings.ContainsKey(a.Type.ToString()))
                {
                    stringBuilder.AppendLine($"\t\t<property column=\"{CamelCaseToUnderscore(a.Identifier.ToString())}\" name=\"{a.Identifier}\" type=\"{TypesForMappings[a.Type.ToString()]}\" />");
                }
                else
                {
                    stringBuilder.AppendLine($"\t\t<property column=\"{CamelCaseToUnderscore(a.Identifier.ToString())}\" name=\"{a.Identifier}\" type=\"{a.Type}\" />");
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
            return c.AccessorList != null &&
                                c.AccessorList.ChildNodes().Select(prop => prop.ToString()).Contains("set;");
        }

        public static string CamelCaseToUnderscore(string camelCase)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(char.ToLower(camelCase[0]));
            for (int i = 1; i < camelCase.Length; i++)
            {
                if (char.IsLetter(camelCase[i]) && char.IsUpper(camelCase[i]))
                {
                    stringBuilder.Append('_');
                    stringBuilder.Append(char.ToLower(camelCase[i]));
                }
                else
                {
                    stringBuilder.Append(camelCase[i]);
                }
            }
            return stringBuilder.ToString();
        }
        //public static string GetTableNamePrefix(string assemblyName)
        //{
        //    assemblyName.
        //    return;
        //}
    }
}
