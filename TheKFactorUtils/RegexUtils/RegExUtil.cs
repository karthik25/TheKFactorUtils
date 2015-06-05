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

        public static string GetFirstMatch(this string src, Regex regex)
        {
            return src.GetNthMatch(regex, 0);
        }

        public static string GetNthMatch(this string src, Regex regex, int n)
        {
            var match = regex.Match(src);
            if (!match.Success || n >= (match.Groups.Count - 1))
            {
                return null;
            }

            var requiredMatch = match.Groups[(n + 1)].Value;
            return requiredMatch;
        }
    }
}