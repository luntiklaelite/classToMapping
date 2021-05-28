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
            var files = Directory.GetFiles(args[0], "*.cs", SearchOption.AllDirectories);
            MappingGenerator gen = new MappingGenerator(args[2], args[3], files)
            {
                MapEnumToIntegerType = true
            };
            //        gen.CustomXmlInMainMapping = @"    <bag name=""Obstacles"" lazy=""false"" cascade=""all-delete-orphan"">
            //      <key column=""bridge_id""/>
            //    <one-to-many class=""ITS.Core.Bridges.Domain.BridgeObstacle, ITS.Core.Bridges""/>
            //</bag>
            //<bag name=""Supports"" lazy =""false"" cascade=""all-delete-orphan"" >
            //  <key column=""bridge_id"" />
            //  <one-to-many class=""ITS.Core.Bridges.Domain.BridgeSupport, ITS.Core.Bridges"" />
            //</bag>
            //<bag name=""SpanStructures"" lazy =""false"" cascade=""all-delete-orphan"" >
            //  <key column=""bridge_id"" />
            //  <one-to-many class=""ITS.Core.Bridges.Domain.SpanStructure, ITS.Core.Bridges"" />
            //</bag>
            //<bag name=""Defects"" lazy =""false"" cascade=""all-delete-orphan"" >
            //  <key column=""bridge_id"" />
            //  <one-to-many class=""ITS.Core.Bridges.Domain.Defect, ITS.Core.Bridges"" />
            //</bag>
            //<bag name=""InfoOfRepairs"" lazy=""false"" cascade=""all-delete-orphan"" >
            //      <key column = ""bridge_id"" />
            //      <one-to-many class=""ITS.Core.Bridges.Domain.InfoOfRepairs, ITS.Core.Bridges"" />
            //</bag>
            //<bag name=""Documentations"" lazy=""false"" cascade=""all-delete-orphan"">
            //      <key column=""bridge_id""/>
            //      <one-to-many class=""ITS.Core.Bridges.Domain.DocumentationInfo, ITS.Core.Bridges""/>
            //</bag>";
            //gen.PropertyWithCascadeAll.AddRange(new[]
            //{
            //    "ProtectionOnBridge",
            //    "ProtectionOnApproach",
            //    "ProtectionOnApproach",
            //    "Section",
            //    "CrossPile",
            //    "LongitudinalPile",
            //});
            var mappings = gen.GenerateMappings();
            Console.WriteLine("Generated files:");
            var relPaths = new string[mappings.Count];
            for (int i = 0; i < mappings.Count; i++)
            {
                relPaths[i] = $"Mappings\\{mappings[i].Key}";
                var path = args[1] + "\\" + relPaths[i];
                Console.WriteLine(path);
                File.WriteAllText(path, mappings[i].Value);
            }
            WriteToCsproj(relPaths, args[1]);

            MigrationGenerator gen1 = new MigrationGenerator("ITS.DbMigration.Core", "adrplan",
                files)
            {
                MapEnumToIntegerType = true,
            //    CustomCodeUp = @"Database.ExecuteNonQuery(Properties.Resources.InsertDefectScrollSections);            
            //Database.ExecuteNonQuery(Properties.Resources.InsertDefectTypes);
            //Database.ExecuteNonQuery(Properties.Resources.InsertMaterials);
            //Database.ExecuteNonQuery(Properties.Resources.InsertTypicalProjects);
            //Database.ExecuteNonQuery(Properties.Resources.InsertTerritories);",
            };
            //gen1.NotMappedPropertyNames.Add("Obstacles");
            //gen1.NotMappedPropertyNames.Add("InfoOfRepairs");
            //gen1.NotMappedPropertyNames.Add("Supports");
            //gen1.NotMappedPropertyNames.Add("SpanStructures");
            //gen1.NotMappedPropertyNames.Add("Defects");
            //gen1.NotMappedPropertyNames.Add("Documentations");

            ClearDir(args);
            var migr = gen1.GenerateMigration();
            Console.WriteLine("Generated migration:");
            var path1 = args[1] + $"\\Migrations\\{gen1.MigrationFileName}";
            File.WriteAllText(path1, migr);
            Console.WriteLine(path1);

            migr = gen1.GenerateEmptyMigration();
            Console.WriteLine("Generated migration:");
            var path2 = args[1] + $"\\Migrations\\{gen1.MigrationFileName}";
            Console.WriteLine(path2);
            File.WriteAllText(path2, migr);

            Console.ReadKey();
        }

        private static void ClearDir(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo(args[1] + $"\\Migrations");
            foreach (var item in di.GetFiles())
            {
                item.Delete();
            }
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
