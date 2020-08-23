using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorsLibrary;

namespace classToMapping
{
    class Program
    {
        //TO DO: сделать разбор входных параметров приложения, 
        //в качестве параметров могут быть пути файлов с исходным кодом,
        //названия свойств, которые не надо вносить в маппинг и другое
        static void Main(string[] args)
        {
            CreateOutputDirectorys();
            if (args.Length != 3)
            {
                Console.WriteLine("Too few arguments.");
                return;
            }
            var files = Directory.GetFiles(args[0],"*.cs",SearchOption.AllDirectories);
            MappingGenerator gen = new MappingGenerator(args[1], args[2], files);
            var mappings = gen.GenerateMappings();
            foreach (var keyValuePair in mappings)
            {
                File.WriteAllText(Environment.CurrentDirectory + $"\\Output\\Mappings\\{keyValuePair.Key}",
                    keyValuePair.Value);
            }
        }

        private static void CreateOutputDirectorys()
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + $"\\Output");
            Directory.CreateDirectory(Environment.CurrentDirectory + $"\\Output\\Mappings");
        }
    }
}
