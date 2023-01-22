namespace ParserByWhoyandog.Core.Aliexpress
{
    internal class SettingsAliexpress : IParserSettings
    {
        public SettingsAliexpress(int startPage, int endPage, string url)
        {
            StartPage= startPage;
            EndPage= endPage;
            BaseURL= url;
        }

        public string BaseURL { get; set; }
        public string Prefix { get; set; } = "&page=";
        public string Template { get; set; } = "{BaseURL}{Prefix}";

        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
