using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace ParserByWhoyandog.Core.Avito
{
    internal class ParserAvito : IParser<string[]>
    {
        public string[] Parse (IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName.Contains("iva-item-body-"));

            foreach (var item in items)
            {
                string href;
                list.Add($"{FindName(item, out href)}, {FindPrice(item)}        https://www.avito.ru{href}");
            }

            string FindName(IElement items, out string href)
            {
                href = "<null>";

                foreach (var i in items.Children)
                {
                    if (i.ClassName != null && i.ClassName.Contains("iva-item-titleStep"))
                    {
                        var elementHref = i.QuerySelector("a").GetAttribute("href");
                        if (elementHref != null)
                            href = elementHref;

                        var element = i.QuerySelector("h3");
                        return element != null ? element.TextContent : "<null>";
                    }
                }

                return "<null>";
            }

            string FindPrice(IElement items)
            {
                foreach (var i in items.Children)
                {
                    if (i.ClassName != null && i.ClassName.Contains("iva-item-priceStep"))
                    {
                        var element = i.QuerySelectorAll("span").Where(item => item != null && item.ClassName.Contains("price-text-")).FirstOrDefault();
                        return element != null ? element.TextContent : "<null>";
                    }
                }

                return "<null>";
            }

            return list.ToArray();
        }
    }
}
