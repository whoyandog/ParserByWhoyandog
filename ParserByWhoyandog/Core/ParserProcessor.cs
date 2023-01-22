using AngleSharp.Html.Parser;
using System;

namespace ParserByWhoyandog.Core
{
    class ParserProcessor<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;

        HtmlLoader loader;

        bool isActive;

        #region Properties

        public IParser<T> Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        #endregion

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public ParserProcessor(IParser<T> parser)
        {
            this.parser = parser;
        }

        public ParserProcessor(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            Settings = parserSettings;
        }

        public void Start()
        {
            isActive = true;
            Processor();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Processor()
        {
            for (int i = parserSettings.StartPage; i <= parserSettings.EndPage; i++)
            {
                if (!isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await loader.GetSourceByPageID(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);

                var result = parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
            isActive = false;
        }
    }
}
