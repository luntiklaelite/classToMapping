using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GeneratorsLibrary;

namespace classToMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Too few arguments.");
                return;
            }
            var files = Directory.GetFiles(args[0],"*.cs",SearchOption.AllDirectories);
            MappingGenerator gen = new MappingGenerator(args[2], args[3], files);
            var mappings = gen.GenerateMappings();
            Console.WriteLine("Generated files:");
            var relPaths = new string[mappings.Count];
            for (int i = 0; i < mappings.Count; i++)
            {
                relPaths[i] = $"Mappings\\{mappings[i].Key}";
                var path = args[1] + "\\" + relPaths[i];
                Console.WriteLine(path);
                File.WriteAllText(path,mappings[i].Value);
            }
            WriteToCsproj(relPaths, args[1]);
            Console.ReadLine();
        }
        public static void WriteToCsproj(string[] relPath, string projectDir)
        {
            var csprojPath = Directory.GetFiles(projectDir, "*.csproj");
            if (csprojPath.Length < 1)
            {
                Console.WriteLine(".csproj not found.");
                return;
            }
            StringBuilder stringBuilder = new StringBuilder();
            
            string csproj;
            using (StreamReader reader = new StreamReader(csprojPath[0]))
            {
                csproj = reader.ReadToEnd();
            }
            bool newFilesToCsproj = false;
            for (int i = 0; i < relPath.Length; i++)
            {
                var isWrited = Regex.IsMatch(csproj, $"{relPath[i].Replace("\\", "\\\\")}");
                if (!isWrited)
                {
                    stringBuilder.AppendLine($"  <ItemGroup> <!--Generated:{DateTime.Now}-->");
                    stringBuilder.AppendLine($@"    <EmbeddedResource Include=""{relPath[i]}"">");
                    stringBuilder.AppendLine($@"      <SubType>Designer</SubType>");
                    stringBuilder.AppendLine($@"    </EmbeddedResource>");
                    stringBuilder.AppendLine($"  </ItemGroup>");
                    newFilesToCsproj = true;
                }
            }
            if (newFilesToCsproj)
            {
                csproj = csproj.Insert(csproj.Length - "</Project>".Length,
                    stringBuilder.ToString());
                using (StreamWriter writer = new StreamWriter(csprojPath[0]))
                {
                    writer.Write(csproj);
                }
                Console.WriteLine(".csproj was modified.");
            }
            else
            {
                Console.WriteLine("files is already written to .csproj");
            }
        }
    }
}
