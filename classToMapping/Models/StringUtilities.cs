using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classToMapping.Models
{
    public static class StringUtilities
    {
        public static string CamelCaseToUnderscore(string camelCase)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(char.ToLower(camelCase[0]));
            for (int i = 1; i < camelCase.Length; i++)
            {
                if (char.IsLetter(camelCase[i]) && char.IsUpper(camelCase[i]))
                {
                    stringBuilder.Append('_');
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
