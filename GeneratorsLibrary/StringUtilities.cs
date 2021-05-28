using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorsLibrary
{
    public static class StringUtilities
    {
        public static string CamelCaseToUnderscore(string camelCase)
        {
            if (string.IsNullOrEmpty(camelCase))
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(char.ToLower(camelCase[0]));
            for (int i = 1; i < camelCase.Length; i++)
            {
                if (char.IsLetter(camelCase[i]) && char.IsUpper(camelCase[i]))
                {
                    if (camelCase[i - 1] != '_' && (i != camelCase.Length - 1 && !char.IsUpper(camelCase[i + 1])))
                    {
                        stringBuilder.Append('_');
                    }
                    stringBuilder.Append(char.ToLower(camelCase[i]));
                }
                else
                {
                    stringBuilder.Append(camelCase[i]);
                }
            }
            return stringBuilder.ToString();
        }
    }
}
