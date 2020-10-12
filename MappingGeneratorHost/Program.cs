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
            MappingGenerator gen = new MappingGenerator(args[2], args[3], files)
            {
                MapEnumToIntegerType = true
            };
            gen.NotMappedPropertyNames.Add("Obstacles");
            gen.NotMappedPropertyNames.Add("Defects");
            gen.CustomXmlInMainMapping = @"     <bag name=""Obstacles"" lazy=""false"" cascade=""all-delete-orphan"">
          <key column = ""bridge_id""/>
        <one-to-many class=""ITS.Core.Bridges.Domain.BridgeObstacle, ITS.Core.Bridges""/>
    </bag>
    <bag name = ""Supports"" lazy =""false"" cascade =""all-delete-orphan"" >
      <key column = ""bridge_id"" />
      <one-to-many class=""ITS.Core.Bridges.Domain.BridgeSupport, ITS.Core.Bridges"" />
    </bag>
    <bag name = ""SpanStructures"" lazy =""false"" cascade =""all-delete-orphan"" >
      <key column = ""bridge_id"" />
      <one-to-many class=""ITS.Core.Bridges.Domain.SpanStructure, ITS.Core.Bridges"" />
    </bag>
    <bag name = ""Defects"" lazy =""false"" cascade =""all-delete-orphan"" >
      <key column = ""bridge_id"" />
      <one-to-many class=""ITS.Core.Bridges.Domain.Defect, ITS.Core.Bridges"" />
    </bag>";
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

            MigrationGenerator gen1 = new MigrationGenerator("ITS.DbMigration.Bridges", "bridges",
                files)
            {
                MapEnumToIntegerType = true,
            };
            gen1.CustomCodeUp = @"Database.ExecuteNonQuery(""INSERT INTO bridges_material(id, name) VALUES (1, 'Железобетон'), (2, 'Бетон'), (3, 'Бутобетон'), (4, 'Каменная или бетонная кладка'), (5, 'Древесина'), (6, 'Железобетон преднапряженный'), (7, 'Сталь'), (8, 'Сталежелезобетон'), (9, 'Древесина клееная'), (10, 'Алюминий'), (11, 'Композитный материал'), (12, 'Прочее'), (13, 'Нет данных')""); ";
            gen1.NotMappedPropertyNames.Add("Obstacles");
            var migr = gen1.GenerateMigration();
            Console.WriteLine("Generated migration:");
            var path1 = args[1] + "\\" + $"Migrations\\{gen1.MigrationFileName}";
            Console.WriteLine(path1);
            File.WriteAllText(path1, migr);
            Console.ReadKey();
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
