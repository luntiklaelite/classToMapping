﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classToMapping.Models
{
    public class EnumGenerator
    {
        public string NamespaceOfEnum { get; set; }
        public string NamespaceOfEnumConverter { get; set; }
        public string NameOfEnum { get; set; }
        public string CommentOfEnum { get; set; }
        public List<string> EnumElements { get; set; }
        public List<string> EnumComments { get; set; }

        public EnumGenerator()
        {

        }
        public EnumGenerator(string enumName, string enumComment, List<string> enumElements, List<string> enumComments) : this(enumName)
        {
            CommentOfEnum = enumComment;
            EnumElements = enumElements;
            EnumComments = enumComments;
        }
        public EnumGenerator(string values)
        {
            EnumElements = new List<string>();
            EnumComments = new List<string>();
            ParseValues(values);
        }
        private void ParseValues(string values)
        {
            var tmp = new string(values.Where(c=>c!='\r').ToArray()).Split('\n');
            NamespaceOfEnum = tmp[0];
            NamespaceOfEnumConverter = tmp[1];
            CommentOfEnum = tmp[2];
            NameOfEnum = tmp[3];
            for (int i = 4; i < tmp.Length; i++)
            {
                if (i % 2 == 0)
                {
                    EnumComments.Add(tmp[i]);
                }
                else
                {
                    EnumElements.Add(tmp[i]);
                }
            }
        }
        public string GetEnumText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"namespace {NamespaceOfEnum}");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("\t/// <summary>");
            stringBuilder.AppendLine($"\t/// {CommentOfEnum}");
            stringBuilder.AppendLine("\t/// </summary>");
            stringBuilder.AppendLine("\t[Serializable]");
            stringBuilder.AppendLine($"\tpublic enum {NameOfEnum}");
            stringBuilder.AppendLine("\t{");
            for (int i = 0; i < EnumElements.Count; i++)
            {
                stringBuilder.AppendLine("\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// {EnumComments[i]}");
                stringBuilder.AppendLine("\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\t{EnumElements[i]},");
            }
            stringBuilder.AppendLine("\t}");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
        public string GetEnumStringConverterText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"using {NamespaceOfEnum};");
            stringBuilder.AppendLine("using System.Collections.Generic;");
            stringBuilder.AppendLine("using System.Linq;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"namespace {NamespaceOfEnumConverter}");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("\t/// <summary>");
            stringBuilder.AppendLine($"\t/// Класс-преобразователь перечисления {NameOfEnum} в строку и обратно");
            stringBuilder.AppendLine("\t/// </summary>");
            stringBuilder.AppendLine($"\tpublic class {NameOfEnum}Strings : IEnumStrings<{NameOfEnum}>");
            stringBuilder.AppendLine("\t{");
            stringBuilder.AppendLine($"\t\t public static Dictionary<{NameOfEnum}, string> Strings =");
            stringBuilder.AppendLine($"\t\t\t new Dictionary<{NameOfEnum}, string> {{");
            for (int i = 0; i < EnumElements.Count; i++)
            {
                stringBuilder.AppendLine($"\t\t\t\t {{ {NameOfEnum}.{EnumElements[i]},\"{EnumComments[i]}\" }},");
            }
            stringBuilder.AppendLine("\t\t\t};");
            stringBuilder.AppendLine($"\t\tpublic {NameOfEnum}Strings() {{ }}");
            stringBuilder.AppendLine($"\t\tprivate static {NameOfEnum}Strings instance;");
            stringBuilder.AppendLine($"\t\tpublic static {NameOfEnum}Strings Instance");    
            stringBuilder.AppendLine($"\t\t{{");
            stringBuilder.AppendLine($"\t\t\tget");
            stringBuilder.AppendLine($"\t\t\t{{");
            stringBuilder.AppendLine($"\t\t\t\tif (instance == null)");
            stringBuilder.AppendLine($"\t\t\t\t{{");
            stringBuilder.AppendLine($"\t\t\t\t\tinstance = new BridgeStatusStrings();");
            stringBuilder.AppendLine($"\t\t\t\t}}");
            stringBuilder.AppendLine($"\t\t\t\treturn instance;");
            stringBuilder.AppendLine($"\t\t\t}}");
            stringBuilder.AppendLine($"\t\t}}");
            stringBuilder.AppendLine();
            var nameOfParam = MappingGenerator.CamelCaseToUnderscore(NameOfEnum);
            stringBuilder.AppendLine($"\t\tpublic string GetName({NameOfEnum} {nameOfParam})");
            stringBuilder.AppendLine($"\t\t{{");
            stringBuilder.AppendLine($"\t\t\treturn Strings[{nameOfParam}];");
            stringBuilder.AppendLine($"\t\t}}");
            stringBuilder.AppendLine($"\t\tpublic {NameOfEnum} GetName(string name)");
            stringBuilder.AppendLine($"\t\t{{");
            stringBuilder.AppendLine($"\t\t\treturn Strings.FirstOrDefault(s => s.Value == name).Key;");
            stringBuilder.AppendLine($"\t\t}}");
            stringBuilder.AppendLine("\t}");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
