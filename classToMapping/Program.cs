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
        static void Main(string[] args)
        {
            Modifer modifer = new Modifer("ITS.Core.Bridges");
            var mappingText = modifer.GenerateMapping(@"namespace ITS.Bridges.Core.Domain
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
            }");
            Console.WriteLine(mappingText);
            File.WriteAllText(Environment.CurrentDirectory+"/"+modifer.FileName,mappingText);
            Console.Read();
        }
    }
}
