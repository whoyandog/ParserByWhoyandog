﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserByWhoyandog.Core.Phpnick
{
    internal class SettingsPhpnick : IParserSettings
    {
        public SettingsPhpnick(int startPage, int endPage, string url)
        {
            StartPage = startPage;
            EndPage = endPage;
            BaseURL = url;
        }

        public string BaseURL { get; set; }
        public string Prefix { get; set; } = "?page=";
        public string Template { get; set; } = "{BaseURL}{Prefix}";
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
