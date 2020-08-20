using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classToMapping
{
    class Program
    {
        //TO DO: сделать разбор входных параметров приложения, 
        //в качестве параметров могут быть пути файлов с исходным кодом,
        //названия свойств, которые не надо вносить в маппинг и другое
        static void Main(string[] args)
        {
            Modifer modifer = new Modifer("ITS.Core.Bridges");
            var csharpCode = @"namespace ITS.Bridges.Core.Domain
            {
                class Example : DomainObject<long>
                {
                    public float HeightToNaturalSoil { get; set; }
                    public string Str1 { get; set; } 
                    public int Int1 { get; set; }
                    public double CrossbarHeight { get; set; }
                    public decimal D2 { get; set; }
                    public DateTime Notes { get; set; }
                    public DateTime? Notes1 { get; set; }
                }
            }";
            modifer.SetParsedTextFromFiles(new[] 
            {
                @"D:\ProjectsRepository\repos\RoadPipes\ITS.Core.RoadPipes\Domain\Pipe.cs",
                @"D:\ProjectsRepository\repos\RoadPipes\ITS.Core.RoadPipes\Domain\Defect.cs",
                @"D:\ProjectsRepository\repos\RoadPipes\ITS.Core.RoadPipes\Domain\PipeEdgeInfo.cs",
                @"D:\ProjectsRepository\repos\RoadPipes\ITS.Core.RoadPipes\Domain\Strengthening.cs",
                @"D:\ProjectsRepository\repos\RoadPipes\ITS.Core.RoadPipes\Domain\Tip.cs",
            });
            var mappingText = modifer.GenerateMapping();
            Console.WriteLine(mappingText);
            File.WriteAllText(Environment.CurrentDirectory+"/"+modifer.FileName,mappingText);
            Console.Read();
        }
    }
}
