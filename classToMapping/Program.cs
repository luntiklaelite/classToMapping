using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using classToMapping.Models;

namespace classToMapping
{
    class Program
    {
        //TO DO: сделать разбор входных параметров приложения, 
        //в качестве параметров могут быть пути файлов с исходным кодом,
        //названия свойств, которые не надо вносить в маппинг и другое
        static void Main(string[] args)
        {
            //MappingGenerator gen = new MappingGenerator("ITS.Core.Bridges");
            //var csharpCode = @"namespace ITS.Bridges.Core.Domain
            //{
            //    class Example : DomainObject<long>
            //    {
            //        public float HeightToNaturalSoil { get; set; }
            //        public string Str1 { get; set; } 
            //        public int Int1 { get; set; }
            //        public double CrossbarHeight { get; set; }
            //        public decimal D2 { get; set; }
            //        public DateTime Notes { get; set; }
            //        public DateTime? Notes1 { get; set; }
            //    }
            //}";
            //gen.SetParsedTextFromFiles(new[] 
            //{
            //    @"D:\ProjectsRepository\repos\RoadPipes\ITS.Core.RoadPipes\Domain\Pipe.cs",
            //    @"D:\ProjectsRepository\repos\RoadPipes\ITS.Core.RoadPipes\Domain\Defect.cs",
            //    @"D:\ProjectsRepository\repos\RoadPipes\ITS.Core.RoadPipes\Domain\PipeEdgeInfo.cs",
            //    @"D:\ProjectsRepository\repos\RoadPipes\ITS.Core.RoadPipes\Domain\Strengthening.cs",
            //    @"D:\ProjectsRepository\repos\RoadPipes\ITS.Core.RoadPipes\Domain\Tip.cs",
            //});
            //var mappingText = gen.GenerateMapping();
            //Console.WriteLine(mappingText);
            //File.WriteAllText(Environment.CurrentDirectory+$"\\{gen.FileName}",mappingText);
            if (Directory.Exists(Environment.CurrentDirectory + $"\\Output"))
            {
                Directory.Delete(Environment.CurrentDirectory + $"\\Output", true);
            }
            Directory.CreateDirectory(Environment.CurrentDirectory + $"\\Output");
            Directory.CreateDirectory(Environment.CurrentDirectory + $"\\Output\\Enums");
            Directory.CreateDirectory(Environment.CurrentDirectory + $"\\Output\\EnumsConverters");
            var files = Directory.GetFiles(Environment.CurrentDirectory + "\\Input","*.txt");
            for (int i = 0; i < files.Length; i++)
            {
                EnumGenerator generator = new EnumGenerator(File.ReadAllText(files[i]));
                string enumText = generator.GetEnumText();
                string enumConverterText = generator.GetEnumStringConverterText();
#if DEBUG
                Console.WriteLine(enumText);
                Console.WriteLine(enumConverterText);
#endif
                File.WriteAllText(Environment.CurrentDirectory + $"\\Output\\Enums\\{generator.NameOfEnum}.cs", enumText);
                File.WriteAllText(Environment.CurrentDirectory + $"\\Output\\EnumsConverters\\{generator.NameOfEnum}Strings.cs", enumConverterText);
            }
#if DEBUG
            Console.Read();
#endif
        }
    }
}
