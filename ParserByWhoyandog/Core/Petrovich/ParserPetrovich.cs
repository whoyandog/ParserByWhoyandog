using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserByWhoyandog.Core.Petrovich
{
    internal class ParserPetrovich : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName.Contains("card-catalog-wide pt-flex pt-justify-between"));

            foreach (var item in items)
            {
                list.Add($"{FindName(item)}, {FindPrice(item)}        https://rf.petrovich.ru{GetHref(item)}");
            }

            string FindName(IElement items)
            {
                var element = items.QuerySelectorAll("span").Where(item => item.ClassName != null && item.ClassName.Contains("pt-typography")).FirstOrDefault();

                return element != null ? element.TextContent : "<null>";
            }

            string FindPrice(IElement items)
            {
                var element = items.QuerySelectorAll("p").Where(item => item.ClassName != null && item.ClassName.Contains("pt-price")).FirstOrDefault();

                return element != null ? element.TextContent : "<null>";
            }

            string GetHref(IElement items)
            {
                var elements = items.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName.Contains("pt-paper")).FirstOrDefault();
                var element = elements.QuerySelectorAll("a").FirstOrDefault().GetAttribute("href");

                return element != null ? element : "<null>";
            }

            return list.ToArray();
        }
    }
}
