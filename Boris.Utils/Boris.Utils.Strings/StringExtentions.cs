using System;

namespace Boris.Utils.Strings
{
    public static class StringExtentions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
