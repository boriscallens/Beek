using System;
using System.IO;

namespace Boris.Utils.IO
{
    public static class IOHelper
    {
        public static string MakeAbsolute(string path)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path.Trim(new[] { '\\', '/' }));
        }

    }
}
