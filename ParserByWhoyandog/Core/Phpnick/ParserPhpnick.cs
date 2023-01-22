using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ParserByWhoyandog.Core.Phpnick
{
    internal class ParserPhpnick : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("article");

            foreach (var item in items)
            {
                string href = "<null>";
                list.Add($"{FindHeader(item, out href)} ({FindTags(item)})        https://phpnick.ru{href}");
            }

            string FindHeader(IElement items, out string href)
            {
                var elements = items.QuerySelector("header");
                var element = elements.QuerySelector("a");

                if (element == null)
                {
                    href = "<null>";
                    return "<null>";
                }

                href = element.GetAttribute("href");
                
                string result = element.TextContent.Trim();

                return result;
            }

            string FindTags(IElement items)
            {
                var elements = items.QuerySelectorAll("li");
                string element = "";

                if (elements == null)
                    return "<null>";

                foreach (var e in elements)
                    element += e.TextContent.Trim() + ", ";

                return element.Remove(element.Length - 2);
            }

            return list.ToArray();
        }
    }
}
