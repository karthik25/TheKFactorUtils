using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TheKFactorUtils.Generators
{
    public static class SlugGenerator
    {
        public static string GetUniqueSlug(this string rawString)
        {
            return rawString.GetUniqueSlug(new List<string>());
        }

        public static string GetUniqueSlug(this string rawString, List<string> currentUrls)
        {
            var regex = new Regex(@"[^a-zA-Z 0-9\.\-]+");

            // Remove any character which is not an alphabet(uppercase/lowercase), number, period, hyphen
            var slug = regex.Replace(rawString.ToLower(), string.Empty)
                            .ReplaceMatches(@"[ ]{1,}") // Replace one or more spaces with a hyphen            
                            .ReplaceMatches(@"[\.]{1,}") // Replace one or more period with a hyphen            
                            .ReplaceMatches(@"[\-]{2,}"); // Replace one or more hyphens with a single hyphen

            if (slug.StartsWith("-"))
                slug = string.Format("0{0}", slug);

            if (slug.EndsWith("-"))
                slug = string.Format("{0}0", slug);

            return currentUrls.Any(s => s == slug) ? GetUniqueSlugInternal(slug, currentUrls)
                                                : slug;
        }

        private static string ReplaceMatches(this string srcString, string pattern)
        {
            var removalPattern = new Regex(pattern);
            return removalPattern.Replace(srcString, "-");
        }

        private static string GetUniqueSlugInternal(string srcString, List<string> srcList)
        {
            var slugRegex = new Regex(string.Format(@"^{0}-([0-9]+)$", srcString));
            var matchingSlugs = new List<int>();
            srcList.ForEach(s =>
            {
                var match = slugRegex.Match(s);
                if (match.Success)
                {
                    var number = int.Parse(match.Groups[1].Captures[0].Value);
                    matchingSlugs.Add(number);
                }
            });
            if (matchingSlugs.Any())
            {
                var max = matchingSlugs.Max();
                return string.Format("{0}-{1}", srcString, max + 1);
            }
            return string.Format("{0}-2", srcString);
        }
    }
}
