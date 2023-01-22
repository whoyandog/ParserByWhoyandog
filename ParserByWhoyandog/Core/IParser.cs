using AngleSharp.Html.Dom;

namespace ParserByWhoyandog.Core
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
