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
            MappingGenerator gen = new MappingGenerator("ITS.Core.RoadPipes","rp");
            var files = Directory.GetFiles(args[0],
                "*.cs",SearchOption.AllDirectories);
            gen.SetParsedTextFromFiles(files);
            var dict = gen.GenerateMappings();
            foreach (var keyValuePair in dict)
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
