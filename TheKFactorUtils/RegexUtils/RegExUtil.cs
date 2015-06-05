using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TheKFactorUtils.RegexUtils
{
    public static class RegExUtil
    {
         public static IEnumerable<string> GetMatchValues(this string src, Regex regex)
         {
             var match = regex.Match(src);
             if (!match.Success)
                 yield break;

             for (var i = 1; i < match.Groups.Count; i++)
             {
                 yield return match.Groups[i].Value;
             }
         }
    }
}