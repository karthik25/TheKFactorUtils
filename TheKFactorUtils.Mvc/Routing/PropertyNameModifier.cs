using System;
using System.Collections.Generic;

namespace TheKFactorUtils.Mvc.Routing
{
    public static class PropertyNameModifier
    {
        public static IEnumerable<string> GetModifiedPropertyNames(this string propertyName)
        {
            yield return propertyName;
            yield return propertyName.ToCamelCase();
            yield return propertyName.ToLower();
        }

        // Credit: http://csharphelper.com/blog/2014/10/convert-between-pascal-case-camel-case-and-proper-case-in-c/
        private static string ToCamelCase(this string str)
        {
            // If there are 0 or 1 characters, just return the string.
            if (str == null || str.Length < 2)
                return str;

            // Split the string into words.
            var words = str.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            var result = words[0].ToLower();
            for (var i = 1; i < words.Length; i++)
            {
                result += words[i].Substring(0, 1).ToUpper() + words[i].Substring(1);
            }

            return result;
        }
    }
}