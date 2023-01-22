using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace ParserByWhoyandog.Core.Aliexpress
{
    internal class ParserAliexpress : IParser<string[]>
    {
        public string[] Parse (IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName.Contains("product-snippet_ProductSnippet__description__"));

            foreach (var item in items)
            {
                list.Add($"{FindName(item)}, {FindPrice(item)}        https://aliexpress.ru{GetHref(item)}");
            }

            string FindName(IElement items)
            {
                var element = items.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName.Contains("product-snippet_ProductSnippet__name__")).FirstOrDefault();

                return element != null ? element.TextContent : "<null>";
            }

            string FindPrice(IElement items)
            {
                var element = items.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName.Contains("snow-price_SnowPrice__mainM__18x8np")).FirstOrDefault();

                return element != null ? element.TextContent : "<null>";
            }

            string GetHref(IElement items)
            {
                var element = items.QuerySelector("a").GetAttribute("href");

                return element != null ? element : "<null>";
            }

            return list.ToArray();
        }
    }
}
