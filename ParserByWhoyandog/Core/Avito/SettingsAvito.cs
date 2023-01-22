namespace ParserByWhoyandog.Core.Avito
{
    internal class SettingsAvito : IParserSettings
    {
        public SettingsAvito(int startPage, int endPage, string url)
        {
            StartPage= startPage;
            EndPage= endPage;
            BaseURL= url;
        }

        public string BaseURL { get; set; }
        public string Prefix { get; set; } = "?p=";
        public string Template { get; set; } = "{BaseURL}{Prefix}";
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
