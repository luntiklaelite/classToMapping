using GeneratorsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EnumGeneratorHost
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Too few arguments.");
                return;
            }
            var files = Directory.GetFiles(args[0], "*.txt");
            if (files.Length < 1)
            {
                Console.WriteLine("Not found files.");
                return;
            }
            var configText = File.ReadAllText(Directory.GetFiles(args[0], "namespaces.cfg")[0]);
            Console.WriteLine("Generated files:");
            string[] relativeNamesEnum = new string[files.Length];
            string[] relativeNamesEnumConverter = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                EnumGenerator generator = new EnumGenerator(File.ReadAllText(files[i]), configText);
                string enumText = generator.GetEnumText();
                string enumConverterText = generator.GetEnumStringConverterText(true);
                string fileRelativeNameEnum = $"Domain\\Enums\\{generator.NameOfEnum}.cs";
                string fileRelativeNameEnumConverter = $"Domain\\EnumStrings\\{generator.NameOfEnum}Strings.cs";
                string fileFullNameEnum = args[1] + "\\" + fileRelativeNameEnum;
                string fileFullNameEnumConverter = args[1] + "\\" + fileRelativeNameEnumConverter;
                File.WriteAllText(fileFullNameEnum, enumText);
                Console.WriteLine(fileFullNameEnum);
                File.WriteAllText(fileFullNameEnumConverter, enumConverterText);
                Console.WriteLine(fileFullNameEnumConverter);
                relativeNamesEnum[i] = fileRelativeNameEnum;
                relativeNamesEnumConverter[i] = fileRelativeNameEnumConverter;
            }
            WriteToCsproj(relativeNamesEnum, relativeNamesEnumConverter, args[1]);
#if DEBUG
            Console.ReadKey();
#endif
        }
        public static void WriteToCsproj(string[] relNamesEnum, string[] relNamesEnumConv, string projectDir)
        {
            var csprojPath = Directory.GetFiles(projectDir, "*.csproj");
            if (csprojPath.Length < 1)
            {
                Console.WriteLine(".csproj not found.");
                return;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"  <ItemGroup> <!--Generated:{DateTime.Now}-->");
            string csproj;
            using (StreamReader reader = new StreamReader(csprojPath[0]))
            {
                csproj = reader.ReadToEnd();
            }
            bool newFilesToCsproj = false;
            for (int i = 0; i < relNamesEnum.Length; i++)
            {
                var writedEnum = Regex.IsMatch(csproj, $"{relNamesEnum[i].Replace("\\", "\\\\")}");
                var writedEnumConverter = Regex.IsMatch(csproj, $"{relNamesEnumConv[i].Replace("\\", "\\\\")}");
                if (!writedEnum)
                {
                    stringBuilder.AppendLine($@"    <Compile Include=""{relNamesEnum[i]}""/>");
                    newFilesToCsproj = true;
                }
                if (!writedEnumConverter)
                {
                    stringBuilder.AppendLine($@"    <Compile Include=""{relNamesEnumConv[i]}""/>");
                    newFilesToCsproj = true;
                }
            }
            if (newFilesToCsproj)
            {
                stringBuilder.AppendLine($"  </ItemGroup>");
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
