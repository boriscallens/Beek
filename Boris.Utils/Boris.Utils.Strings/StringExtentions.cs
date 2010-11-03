using System;
using System.Collections.Generic;
using System.Linq;

namespace Boris.Utils.Strings
{
    public static class StringExtentions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            if(string.IsNullOrEmpty(source) || string.IsNullOrEmpty(toCheck))
            {
                return false;
            }
            return source.IndexOf(toCheck, comp) >= 0;
        }
        public static string Remove(this string source, IEnumerable<char> charsToRemove)
        {
            if (string.IsNullOrEmpty(source) || !charsToRemove.Any())
            {
                return source;
            }
            return new string(source.ToCharArray().Where(c => !charsToRemove.Contains(c)).ToArray());
        }
        public static string Remove(this string source, string stringToRemove)
        {
            return source.Replace(stringToRemove, string.Empty);
        }
    }
}
