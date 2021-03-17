using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneratorsLibrary.StringUtilities;

namespace GeneratorsLibrary
{
    public class EnumGenerator
    {
        public string NamespaceOfEnum { get; set; }
        public string NamespaceOfEnumConverter { get; set; }
        public string FullNameOfBaseClass { get; set; }


        public string NameOfEnum { get; set; }
        public string CommentOfEnum { get; set; }
        public List<string> EnumElements { get; set; } = new List<string>();
        public List<string> EnumComments { get; set; } = new List<string>();
        public string Type { get; private set; }
        private static List<string> AllowedOptions = new List<string>() { "[Flags]", "[IgnoreCase]" };
        private List<string> Options { get; set; } = new List<string>();

        public EnumGenerator()
        {

        }
        public EnumGenerator(string enumName, string enumComment, List<string> enumElements, List<string> enumComments)
            : this()
        {
            NameOfEnum = enumName;
            CommentOfEnum = enumComment;
            EnumElements = enumElements;
            EnumComments = enumComments;
            SetTypeString();
        }
        public EnumGenerator(string values, string configText)
        {
            SetEnumFromConfig(configText);
            ParseText(values);
            SetTypeString();
        }

        private void SetTypeString()
        {
            int count = EnumElements.Count;
            if (Options.Contains("[Flags]"))
            {
                count = (int)Math.Pow(2, count - 2);
            }
            if (count < byte.MaxValue)
            {
                Type = "byte";
            }
            else if (count < short.MaxValue)
            {
                Type = "short";
            }
            else if (count < int.MaxValue)
            {
                Type = "int";
            }
            else
            {
                Type = "long";
            }
        }

        public void SetEnumFromConfig(string configText)
        {
            var tmp = new string(configText.Where(c => c != '\r').ToArray()).Split('\n');
            NamespaceOfEnum = tmp[0];
            NamespaceOfEnumConverter = tmp[1];
            FullNameOfBaseClass = tmp[2];
        }
        public void ParseText(string values)
        {
            var tmp = new string(values.Where(c => c != '\r').ToArray()).Split('\n');
            int startIndex = 0;
            bool comment = true;
            Options = GetOptions(tmp, startIndex);
            if (Options.Count > 0)
            {
                startIndex++;
            }
            CommentOfEnum = tmp[startIndex++];
            NameOfEnum = tmp[startIndex++];
            for (int i = startIndex; i < tmp.Length; i++)
            {
                if (comment)
                {
                    EnumComments.Add(tmp[i].Trim(' '));
                }
                else
                {
                    EnumElements.Add(tmp[i].Trim(' '));
                }
                comment = !comment;
            }
        }

        private static List<string> GetOptions(string[] tmp, int startIndex)
        {
            var a = tmp[startIndex].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var res = new List<string>();
            foreach (var item in a)
            {
                if (AllowedOptions.Contains(item))
                {
                    res.Add(item);
                }
            }
            return res;
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
            if (Options.Contains("[Flags]"))
            {
                stringBuilder.AppendLine("\t[Flags]");
            }
            stringBuilder.AppendLine($"\tpublic enum {NameOfEnum} : {Type}");
            stringBuilder.AppendLine("\t{");
            if (Options.Contains("[Flags]"))
            {
                stringBuilder.AppendLine("\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Нет данных");
                stringBuilder.AppendLine("\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\tNoData = 0,");
                for (int i = 0; i < EnumElements.Count; i++)
                {
                    stringBuilder.AppendLine("\t\t/// <summary>");
                    stringBuilder.AppendLine($"\t\t/// {EnumComments[i]}");
                    stringBuilder.AppendLine("\t\t/// </summary>");
                    stringBuilder.AppendLine($"\t\t{EnumElements[i]} = {(int)Math.Pow(2, i)},");
                }
            }
            else
            {
                for (int i = 0; i < EnumElements.Count; i++)
                {
                    stringBuilder.AppendLine("\t\t/// <summary>");
                    stringBuilder.AppendLine($"\t\t/// {EnumComments[i]}");
                    stringBuilder.AppendLine("\t\t/// </summary>");
                    stringBuilder.AppendLine($"\t\t{EnumElements[i]},");
                }
            }
            stringBuilder.AppendLine("\t}");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
        public string GetEnumStringConverterText(bool needMaxPerfomance = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (NamespaceOfEnum != NamespaceOfEnumConverter)
            {
                stringBuilder.AppendLine($"using {NamespaceOfEnum};");
            }
            stringBuilder.AppendLine("using System;");
            if (Options.Contains("[Flags]"))
                stringBuilder.AppendLine("using System.Text;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"namespace {NamespaceOfEnumConverter}");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("\t/// <summary>");
            stringBuilder.AppendLine($"\t/// Класс-преобразователь перечисления {NameOfEnum} ({CommentOfEnum}) в строку и обратно");
            stringBuilder.AppendLine("\t/// </summary>");
            stringBuilder.AppendLine($"\tpublic class {NameOfEnum}Strings : {FullNameOfBaseClass}<{NameOfEnum}>");
            stringBuilder.AppendLine("\t{");
            if (!needMaxPerfomance)
            {
                stringBuilder.AppendLine($"\t\t public static Dictionary<{NameOfEnum}, string> Strings =");
                stringBuilder.AppendLine($"\t\t\t new Dictionary<{NameOfEnum}, string> {{");
                for (int i = 0; i < EnumElements.Count; i++)
                {
                    stringBuilder.AppendLine($"\t\t\t\t {{ {NameOfEnum}.{EnumElements[i]},\"{EnumComments[i]}\" }},");
                }
                stringBuilder.AppendLine("\t\t\t};");
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Конструктор по-умолчанию");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\tpublic {NameOfEnum}Strings() {{ }}");
                stringBuilder.AppendLine($"\t\tprivate static {NameOfEnum}Strings instance;");
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Свойство для обращения к методам без создания нового экземпляра этого класса");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\tpublic static {NameOfEnum}Strings Instance");
                stringBuilder.AppendLine($"\t\t\t=> instance ?? (instance = new {NameOfEnum}Strings());");
                stringBuilder.AppendLine();
                var nameOfParam = "enumElement";
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Метод для получения строкового представления элемента перечисления {NameOfEnum} ({CommentOfEnum})");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\t/// <param name=\"{nameOfParam}\">Элемент перечисления</param>");
                stringBuilder.AppendLine($"\t\t/// <returns>Строка-результат преобразования</returns>");
                stringBuilder.AppendLine($"\t\tpublic override string GetFullName({NameOfEnum} {nameOfParam})");
                stringBuilder.AppendLine($"\t\t{{");
                stringBuilder.AppendLine($"\t\t\treturn Strings[{nameOfParam}];");
                stringBuilder.AppendLine($"\t\t}}");
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Метод для элемента перечисления {NameOfEnum} ({CommentOfEnum}) из строкового представления");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\t/// <param name=\"name\">Строковое представление элемента перечисления</param>");
                stringBuilder.AppendLine($"\t\t/// <returns>Соответствующий элемент перечисления</returns>");
                stringBuilder.AppendLine($"\t\tpublic override {NameOfEnum} GetElement(string name)");
                stringBuilder.AppendLine($"\t\t{{");
                stringBuilder.AppendLine($"\t\t\treturn Strings.FirstOrDefault(s => s.Value == name).Key;");
                stringBuilder.AppendLine($"\t\t}}");
            }
            else if(Options.Contains("[Flags]"))
            {
                for (int i = 0; i < EnumElements.Count; i++)
                {
                    stringBuilder.AppendLine($"\t\t/// <summary>");
                    stringBuilder.AppendLine($"\t\t/// {EnumComments[i]}");
                    stringBuilder.AppendLine($"\t\t/// </summary>");
                    stringBuilder.AppendLine($"\t\tprivate static readonly string Str{EnumElements[i]} = \"{EnumComments[i]}\";");
                }
                stringBuilder.AppendLine($"\t\tprivate readonly StringBuilder stringBuilder = new StringBuilder();");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Конструктор по-умолчанию");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\tpublic {NameOfEnum}Strings() {{ }}");
                stringBuilder.AppendLine($"\t\tprivate static {NameOfEnum}Strings instance;");
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Свойство для обращения к методам без создания нового экземпляра этого класса");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\tpublic static {NameOfEnum}Strings Instance");
                stringBuilder.AppendLine($"\t\t\t=> instance ?? (instance = new {NameOfEnum}Strings());");
                stringBuilder.AppendLine();
                var nameOfParam = CamelCaseToUnderscore(NameOfEnum);
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Метод для получения строкового представления элемента перечисления {NameOfEnum} ({CommentOfEnum})");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\t/// <param name=\"{nameOfParam}\">Элемент перечисления</param>");
                stringBuilder.AppendLine($"\t\t/// <returns>Строка-результат преобразования</returns>");
                stringBuilder.AppendLine($"\t\tpublic override string GetFullName({NameOfEnum} {nameOfParam})");
                stringBuilder.AppendLine($"\t\t{{");
                stringBuilder.AppendLine($"\t\t\tstringBuilder.Clear();");
                stringBuilder.AppendLine($"\t\t\tbool first = true;");
                stringBuilder.AppendLine($"\t\t\tif({nameOfParam} == {NameOfEnum}.NoData)");
                stringBuilder.AppendLine($"\t\t\t{{");
                stringBuilder.AppendLine($"\t\t\t\treturn \"Нет данных\";");
                stringBuilder.AppendLine($"\t\t\t}}");
                for (int i = 0; i < EnumElements.Count; i++)
                {
                    stringBuilder.AppendLine($"\t\t\tif (({nameOfParam} & {NameOfEnum}.{EnumElements[i]}) == {NameOfEnum}.{EnumElements[i]})");
                    stringBuilder.AppendLine($"\t\t\t{{");
                    stringBuilder.AppendLine($"\t\t\t\tif(!first) stringBuilder.Append(\", \");");
                    stringBuilder.AppendLine($"\t\t\t\tstringBuilder.Append(Str{EnumElements[i]});");
                    stringBuilder.AppendLine($"\t\t\t\tfirst = false;");
                    stringBuilder.AppendLine($"\t\t\t}}");
                }
                stringBuilder.AppendLine($"\t\t\treturn stringBuilder.ToString();");
                stringBuilder.AppendLine($"\t\t}}");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Метод для элемента перечисления {NameOfEnum} ({CommentOfEnum}) из строкового представления");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\t/// <param name=\"name\">Строковое представление элемента перечисления</param>");
                stringBuilder.AppendLine($"\t\t/// <returns>Соответствующий элемент перечисления</returns>");
                stringBuilder.AppendLine($"\t\tpublic override {NameOfEnum} GetElement(string name)");
                stringBuilder.AppendLine($"\t\t{{");
                stringBuilder.AppendLine($"\t\t\tvar tmp = name.Split(new[] {{ \", \" }}, StringSplitOptions.RemoveEmptyEntries);");
                stringBuilder.AppendLine($"\t\t\tvar res = {NameOfEnum}.NoData;");
                stringBuilder.AppendLine($"\t\t\tforeach (var item in tmp)");
                stringBuilder.AppendLine($"\t\t\t{{");
                for (int i = 0; i < EnumElements.Count; i++)
                {
                    if (i == 0)
                    {
                        if (Options.Contains("[IgnoreCase]"))
                            stringBuilder.AppendLine($"\t\t\t\tif (item.Equals(Str{EnumElements[i]}, StringComparison.OrdinalIgnoreCase))");
                        else
                            stringBuilder.AppendLine($"\t\t\t\tif (item == Str{EnumElements[i]})");
                        stringBuilder.AppendLine($"\t\t\t\t\tres |= {NameOfEnum}.{EnumElements[i]};");
                    }
                    else
                    {
                        if (Options.Contains("[IgnoreCase]"))
                            stringBuilder.AppendLine($"\t\t\t\telse if (item.Equals(Str{EnumElements[i]}, StringComparison.OrdinalIgnoreCase))");
                        else
                            stringBuilder.AppendLine($"\t\t\t\telse if (item == Str{EnumElements[i]})");
                        stringBuilder.AppendLine($"\t\t\t\t\tres |= {NameOfEnum}.{EnumElements[i]};");
                    }
                }
                stringBuilder.AppendLine($"\t\t\t}}");
                stringBuilder.AppendLine($"\t\t\treturn res;");
                stringBuilder.AppendLine($"\t\t}}");
            }
            else
            {
                for (int i = 0; i < EnumElements.Count; i++)
                {
                    stringBuilder.AppendLine($"\t\t/// <summary>");
                    stringBuilder.AppendLine($"\t\t/// {EnumComments[i]}");
                    stringBuilder.AppendLine($"\t\t/// </summary>");
                    stringBuilder.AppendLine($"\t\tprivate static readonly string Str{EnumElements[i]} = \"{EnumComments[i]}\";");
                }
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Конструктор по-умолчанию");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\tpublic {NameOfEnum}Strings() {{ }}");
                stringBuilder.AppendLine($"\t\tprivate static {NameOfEnum}Strings instance;");
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Свойство для обращения к методам без создания нового экземпляра этого класса");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\tpublic static {NameOfEnum}Strings Instance");
                stringBuilder.AppendLine($"\t\t\t=> instance ?? (instance = new {NameOfEnum}Strings());");
                stringBuilder.AppendLine();
                var nameOfParam = "enumElement";
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Метод для получения строкового представления элемента перечисления {NameOfEnum} ({CommentOfEnum})");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\t/// <param name=\"{nameOfParam}\">Элемент перечисления</param>");
                stringBuilder.AppendLine($"\t\t/// <returns>Строка-результат преобразования</returns>");
                stringBuilder.AppendLine($"\t\tpublic override string GetFullName({NameOfEnum} {nameOfParam})");
                stringBuilder.AppendLine($"\t\t{{");
                stringBuilder.AppendLine($"\t\t\tswitch({nameOfParam})");
                stringBuilder.AppendLine($"\t\t\t{{");
                for (int i = 0; i < EnumElements.Count; i++)
                {
                    stringBuilder.AppendLine($"\t\t\t\tcase {NameOfEnum}.{EnumElements[i]}:");
                    stringBuilder.AppendLine($"\t\t\t\t\treturn Str{EnumElements[i]};");
                }
                stringBuilder.AppendLine($"\t\t\t}}");
                stringBuilder.AppendLine($"\t\t\tthrow new ArgumentException(\"Некорректный элемент перечисления\", \"{nameOfParam}\");");
                stringBuilder.AppendLine($"\t\t}}");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"\t\t/// <summary>");
                stringBuilder.AppendLine($"\t\t/// Метод для элемента перечисления {NameOfEnum} ({CommentOfEnum}) из строкового представления");
                stringBuilder.AppendLine($"\t\t/// </summary>");
                stringBuilder.AppendLine($"\t\t/// <param name=\"name\">Строковое представление элемента перечисления</param>");
                stringBuilder.AppendLine($"\t\t/// <returns>Соответствующий элемент перечисления</returns>");
                stringBuilder.AppendLine($"\t\tpublic override {NameOfEnum} GetElement(string name)");
                stringBuilder.AppendLine($"\t\t{{");
                for (int i = 0; i < EnumElements.Count; i++)
                {
                    if (Options.Contains("[IgnoreCase]"))
                        stringBuilder.AppendLine($"\t\t\tif (name.Equals(Str{EnumElements[i]}, StringComparison.OrdinalIgnoreCase))");
                    else
                        stringBuilder.AppendLine($"\t\t\tif (name == Str{EnumElements[i]})");
                    stringBuilder.AppendLine($"\t\t\t\treturn {NameOfEnum}.{EnumElements[i]};");
                }
                stringBuilder.AppendLine($"\t\t\tthrow new ArgumentException(\"Некорректная входная строка\", \"name\");");
                stringBuilder.AppendLine($"\t\t}}");
            }
            stringBuilder.AppendLine("\t}");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
