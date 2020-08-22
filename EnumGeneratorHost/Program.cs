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
            var files = Directory.GetFiles(Environment.CurrentDirectory + "\\Input", "*.txt");
            for (int i = 0; i < files.Length; i++)
            {
                EnumGenerator generator = new EnumGenerator(File.ReadAllText(files[i]));
                string enumText = generator.GetEnumText();
                string enumConverterText = generator.GetEnumStringConverterText();
#if DEBUG
                Console.WriteLine(enumText);
                Console.WriteLine("======================================================");
                Console.WriteLine(enumConverterText);
#endif
                File.WriteAllText(Environment.CurrentDirectory + $"\\Output\\Enums\\{generator.NameOfEnum}.cs", enumText);
                File.WriteAllText(Environment.CurrentDirectory + $"\\Output\\EnumsConverters\\{generator.NameOfEnum}Strings.cs", enumConverterText);
            }
#if DEBUG
            Console.Read();
#endif
        }
        private static void CreateOutputDirectorys()
        {
            if (Directory.Exists(Environment.CurrentDirectory + $"\\Output"))
            {
                Directory.Delete(Environment.CurrentDirectory + $"\\Output", true);
            }
            Directory.CreateDirectory(Environment.CurrentDirectory + $"\\Output");
            Directory.CreateDirectory(Environment.CurrentDirectory + $"\\Output\\Enums");
            Directory.CreateDirectory(Environment.CurrentDirectory + $"\\Output\\EnumsConverters");
        }
    }
}
