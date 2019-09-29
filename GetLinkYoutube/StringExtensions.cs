using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetLinkYoutube
{
    public static class StringExtensions
    {
        public static string UrlDecode(this string str)
        {
            return System.Net.WebUtility.UrlDecode(str).Replace("&amp;", "&");
        }

        public static List<string> GetMatchItemsRegex(this string html, string pattern, bool singleLine = false)
        {
            List<string> ls = new List<string>();
            try
            {
                var options = RegexOptions.IgnoreCase;
                if (singleLine)
                    options = options | RegexOptions.Multiline;
                foreach (Match m in Regex.Matches(html, pattern, options))
                {
                    ls.Add(m.Value);
                }
            }
            catch { }
            return ls;
        }

        public static string GetRegexMatchValue(this string input, string regex)
        {
            try
            {
                return Regex.Match(input, regex).Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
