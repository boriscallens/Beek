﻿using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;

namespace Boris.Utils.Html.Helpers
{
    public static class StyleSheetHelper
    {
        private const string styleSheetMask = @"<link rel='stylesheet' href='{0}' type='text/css' media='{1}'>";
        private const string hrefMask = "~/content/css/{0}.css";

        public static HashSet<string> StyleSheets { get; set; }
        public static void AddStyleSheet(this HtmlHelper html, string sheetName)
        {
            if(StyleSheets == null)
            {
                StyleSheets = new HashSet<string>();
            }
            StyleSheets.Add(sheetName);
        }
        public static string PrintStyleSheet(this HtmlHelper html, string styleSheetName)
        {
            var href = string.Format(UrlHelper.GenerateContentUrl(hrefMask, html.ViewContext.HttpContext), styleSheetName);
            return string.Format(styleSheetMask, href, "all");
        }
        public static string OutputStyleSheet(this HtmlHelper html)
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

    public static class TitleHelper
    {
        private const string titleMask = @"<title>{0}</title>";
        private const string tempKey = "title-";
        public static void AddTitlePart(this HtmlHelper html, int index, string part)
        {
            html.ViewData.Add(tempKey + index, part);
        }
        public static void AddTitlePart(this HtmlHelper html, string part)
        {
            // Find the number of parts in order to know the index
            // If no parts are there yet, the index is 0
            IEnumerable<KeyValuePair<string, object>> pairs
                = html.ViewData.Where(kv => kv.Key.StartsWith(tempKey));
            int idx = pairs.Any() ? pairs.Count() : 0;

            AddTitlePart(html, idx, part);
        }
        public static string OutputTitle(this HtmlHelper html)
        {
            List<string> parts = html.ViewData
                                    .Where(kv => kv.Key.StartsWith(tempKey))
                                    .OrderBy(kv => kv.Key)
                                    .Select(kv => kv.Value as string)
                                    .ToList();
            
            StringBuilder sb = new StringBuilder(parts.Count());
            if (parts.Any())
            {
                sb.Append(parts[0]);
                for (int i = 1; i < parts.Count; i++)
                {
                    sb.AppendFormat(" - {0}", parts[i]);
                }
            }
            return string.Format(titleMask, sb);
        }
    }

    public static class ScriptHelper
    {
        private const string scriptMask = @"<script src='{0}' type='{1}'></script>";
        private const string hrefMask = "~/content/js/{0}.js";

        public static HashSet<string> Scripts { get; set; }
        public static void AddScript(string scriptName)
        {
            if (Scripts == null)
            {
                Scripts = new HashSet<string>();
            }
            Scripts.Add(scriptName);
        }
        public static string PrintScript(this HtmlHelper html, string scriptName)
        {
            var href = String.Format(UrlHelper.GenerateContentUrl(hrefMask, html.ViewContext.HttpContext), scriptName);
            return string.Format(scriptMask, href, "text/javascript");
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

    public static class DropDownHelper
    {
        //There's no such thing as Function<T>() where T:Enum
        public static IEnumerable<SelectListItem> SelectListItemsFromEnum<TEnum>(this HtmlHelper html, int selectedItem)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new NotSupportedException("TResult must be an Enum");
            }
            Type type = typeof (TEnum);
            var enumValues = type.GetEnumValues();
            var enumNames = type.GetEnumNames();
            var count = enumNames.Length;
            var enumDescriptions = new string[count];
            int i = 0;
            foreach (var item in enumValues)
            {
                var name = enumNames[i].Trim();
                var fieldInfo = item.GetType().GetField(name);
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                enumDescriptions[i] = (attributes.Length > 0) ? attributes[0].Description : name;
                i++;
            }

            var list = new SelectListItem[count];
            for (int index = 0; index < list.Length; index++)
            {
                list[index] = new SelectListItem { Value = enumNames[index], Text = enumDescriptions[index], Selected = (index == (selectedItem - 1)) };
            }
            return list;
        }
    }
}