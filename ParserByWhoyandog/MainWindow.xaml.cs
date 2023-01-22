using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ParserByWhoyandog.Core;
using ParserByWhoyandog.Core.Aliexpress;
using ParserByWhoyandog.Core.Avito;
using ParserByWhoyandog.Core.Phpnick;
using ParserByWhoyandog.Core.Petrovich;

namespace ParserByWhoyandog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ParserProcessor<string[]> parser;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool ChangeData(string url)
        {
            if (textBoxStart.Text.Trim() == string.Empty)
                textBoxStart.Text = "1";

            if (textBoxEnd.Text.Trim() == string.Empty)
                textBoxEnd.Text = "1";

            textBoxUrl.Text = url;

            return true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void comboBoxParsingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboBoxParsingList.SelectedIndex)
            {
                case 0:
                    ChangeData("https://www.avito.ru/ufa/kvartiry");
                    break;
                case 1:
                    ChangeData("https://rf.petrovich.ru/catalog/12101/");
                    break;
                case 2:
                    ChangeData("https://phpnick.ru/?page=1");
                    break;
            }
        }

        private void ParsingTemplate(IParser<string[]> parser, IParserSettings parserSettings)
        {
            this.parser = new ParserProcessor<string[]>(parser, parserSettings);

            this.parser.OnCompleted += Parser_OnCompleted;
            this.parser.OnNewData += Parser_OnNewData;

            this.parser.Start();
        }


        private void ButtonStart(object sender, RoutedEventArgs e)
        {
            textBlockStatus.Text = "В работе";

            switch (comboBoxParsingList.SelectedIndex)
            {
                case 0:
                    ParsingTemplate(
                        new ParserAvito(),
                        new SettingsAvito(
                            Convert.ToInt32(textBoxStart.Text),
                            Convert.ToInt32(textBoxEnd.Text),
                            textBoxUrl.Text
                            )
                        );
                    break;
                case 1:
                    ParsingTemplate(
                        new ParserPetrovich(),
                        new SettingsPetrovich(
                            Convert.ToInt32(textBoxStart.Text),
                            Convert.ToInt32(textBoxEnd.Text),
                            textBoxUrl.Text
                            )
                        );
                    break;
                case 2:
                    ParsingTemplate(
                        new ParserPhpnick(),
                        new SettingsPhpnick(
                            Convert.ToInt32(textBoxStart.Text),
                            Convert.ToInt32(textBoxEnd.Text),
                            textBoxUrl.Text
                            )
                        );
                    break;
            }
        }

        private void ButtonAbort(object sender, RoutedEventArgs e)
        {
            if (parser != null)
                parser.Abort();
        }
        
        private void ButtonClear(object sender, RoutedEventArgs e)
        {
            textBoxOutputParsingInfo.Clear();
        }

        private void Parser_OnCompleted(object obj)
        {
            textBlockStatus.Text = "Завершено";
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            foreach (var a in arg2)
                textBoxOutputParsingInfo.Text += a + "\n";
        }
    }
}
