namespace ParserByWhoyandog.Core
{
    interface IParserSettings
    {
        string BaseURL { get; set; }
        string Prefix { get; set; }
        string Template { get; set; }
        int StartPage { get; set; }
        int EndPage { get; set; }
    }
}
