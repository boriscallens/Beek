using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boris.Utils.NHaml.Helpers
{
    public static class StyleSheetHelper
    {
        private const string styleSheetMask = 
            @"<link rel='stylesheet' href='/content/css/{0}.css' type='text/css' media='{1}'/>";
        public static HashSet<string> StyleSheets { get; set; }
        public static void AddStyleSheet(string sheetName)
        {
            if(StyleSheets == null)
            {
                StyleSheets = new HashSet<string>();
            }
            StyleSheets.Add(sheetName);
        }
        public static string OutputStyleSheet()
        {
            return OutputStyleSheet("all");
        }
        public static string OutputStyleSheet(string mediaType)
        {
            if (StyleSheets == null || !StyleSheets.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (var styleSheet in StyleSheets)
            {
                sb.AppendFormat(styleSheetMask, styleSheet, mediaType);
            }
            return sb.ToString();
        }
    }
    public static class ScriptHelper
    {
        private const string scriptMask = @"<script src='/content/js/{0}.js' type='{1}'></script>";
        public static HashSet<string> Scripts { get; set; }
        public static void AddScript(string scriptName)
        {
            if (Scripts == null)
            {
                Scripts = new HashSet<string>();
            }
            Scripts.Add(scriptName);
        }
        public static string OutputScripts()
        {
            return OutputScripts("text/javascript");
        }
        public static string OutputScripts(string scriptType)
        {
            if (Scripts == null || !Scripts.Any())
                return string.Empty;            
            StringBuilder sb = new StringBuilder();
            foreach (var script in Scripts)
            {
                sb.AppendFormat(scriptMask, script, scriptType);
            }
            return sb.ToString();
        }
    }
}
