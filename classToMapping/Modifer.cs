using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace classToMapping
{
    public class Modifer
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
        };
        public string AssemblyName { get; set; }
        public string FileName { get; set; }
        public Modifer()
        {

        }
        public Modifer(string assemblyName)
        {
            AssemblyName = assemblyName;
        }
        public string GenerateMapping(string csCode)
        {
            StringBuilder stringBuilder = new StringBuilder();
            SyntaxTree tree = CSharpSyntaxTree.ParseText(csCode);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            var nameSpace = root.ChildNodes().OfType<NamespaceDeclarationSyntax>().First();
            var mainClass = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();
            var name = mainClass.Identifier.ToString();
            var props = mainClass.ChildNodes().OfType<PropertyDeclarationSyntax>();
            var baseList = mainClass.BaseList?.Types.Select(t => t.ToString());

            FileName = mainClass.Identifier.ToString() + ".hbm.xml";

            var propertiesWithPredefinedTypes = root.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(c => c.Parent is ClassDeclarationSyntax)
                .Where(c => c.DescendantNodes().OfType<PredefinedTypeSyntax>().Count() > 0);
            var properties = root.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(c => c.Parent is ClassDeclarationSyntax)
                .Where(c => c.DescendantNodes().OfType<IdentifierNameSyntax>().Count() > 0);
            stringBuilder.AppendLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            stringBuilder.AppendLine($"<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\">");
            stringBuilder.AppendLine($"\t<class lazy=\"false\" name=\"{nameSpace.Name}.{name}, {AssemblyName}\" table=\"{AssemblyName}_{CamelCaseToUnderscore(name)}\">");
            if (baseList.Contains("DomainObject<long>"))
            {
                stringBuilder.AppendLine($"\t\t<id name=\"ID\" column=\"id\" type=\"long\" unsaved-value=\"0\">");
                stringBuilder.AppendLine($"\t\t\t<generator class=\"hilo\" />");
                stringBuilder.AppendLine($"\t\t</id>");
            }
            foreach (var a in properties)
            {
                stringBuilder.AppendLine($"\t\t<many-to-one column=\"{CamelCaseToUnderscore(a.Identifier.ToString())}_id\" name=\"{a.Identifier}\" class=\"{TypesForMappings[a.Type.ToString()]}\"/>");
            }
            foreach (var a in propertiesWithPredefinedTypes)
            {
                if (TypesForMappings[a.Type.ToString()] != null)
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
        //public static string GetShortAssemblyName(string assemblyName)
        //{
        //    assemblyName.
        //    ret
        //}
    }
}
