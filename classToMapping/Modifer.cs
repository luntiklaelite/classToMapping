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
    public static class Modifer
    {
        public static string fromClassToMapping(string toModify)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(toModify);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            var properties = root.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(c => c.Parent is ClassDeclarationSyntax)
                .Where(c => c.DescendantNodes().OfType<PredefinedTypeSyntax>().Count() > 0);
            var classes = root.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(c => c.Parent is ClassDeclarationSyntax)
                .Where(c => c.DescendantNodes().OfType<IdentifierNameSyntax>().Count() > 0);
            foreach(var a in classes)
            {
                Console.WriteLine();
                string s = $"<many-to-one column=\"{a.Identifier}_id\" name=\"{a.Identifier}\" \nclass=\"дописывай ручками\"/>";
                Console.Write(s);
            }
            foreach (var a in properties)
            {
                Console.WriteLine();
                string s;
                if (a.Type.ToString() == "float")
                {
                    s = $"<property column=\"{a.Identifier}\" name=\"{a.Identifier}\" type=\"single\" />";
                }
                else
                {
                    s = $"<property column=\"{a.Identifier}\" name=\"{a.Identifier}\" type=\"{a.Type}\" />";
                }
                Console.Write(s);
            }
            
            return null;
        }
    }
}
