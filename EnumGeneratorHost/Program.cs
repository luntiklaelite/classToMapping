using GeneratorsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumGeneratorHost
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
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
            EnumGenerator generator = new EnumGenerator();
            Console.WriteLine("Generated files:");
            for (int i = 0; i < files.Length; i++)
            {
                generator.ParseText(File.ReadAllText(files[i]));
                string enumText = generator.GetEnumText();
                string enumConverterText = generator.GetEnumStringConverterText();
                string fileFullNameEnum = args[1] + $"\\{generator.NameOfEnum}.cs";
                string fileFullNameEnumConverter = args[2] + $"\\{generator.NameOfEnum}Strings.cs";
                File.WriteAllText(fileFullNameEnum, enumText);
                Console.WriteLine(fileFullNameEnum);
                File.WriteAllText(fileFullNameEnumConverter, enumConverterText);
                Console.WriteLine(fileFullNameEnumConverter);
            }
#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
