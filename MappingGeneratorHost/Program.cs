﻿using System;
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
            Console.WriteLine("Generated files:");
            foreach (var keyValuePair in mappings)
            {
                var path = Environment.CurrentDirectory + $"\\Output\\Mappings\\{keyValuePair.Key}";
                Console.WriteLine(path);
                File.WriteAllText(path,
                    keyValuePair.Value);
            }
            Console.ReadLine();
        }

        private static void CreateOutputDirectorys()
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + $"\\Output");
            Directory.CreateDirectory(Environment.CurrentDirectory + $"\\Output\\Mappings");
        }
    }
}
