using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using AngleSharp.Text;
using System.Web;
using System.Resources;
using System.IO.Packaging;
using AngleSharp.Io;
using System.Windows;

namespace ParserByWhoyandog.Core
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;
        readonly string prefix;

        public HtmlLoader(IParserSettings settings)
        {
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = true
            };

            client = new HttpClient(handler);

            this.prefix = settings.Prefix;
            this.url = UrlReplacement(settings.BaseURL, settings.Prefix, settings.Template);
        }

        public async Task<string> GetSourceByPageID(int id)
        {
            var currentUrl = url.Insert(url.IndexOf(prefix) + prefix.Length, id.ToString());
            HttpResponseMessage response = await client.GetAsync(currentUrl);

            try
            {
                response = await client.GetAsync(currentUrl);
            }
            catch
            {
                MessageBox.Show("Ошибка запроса, попробуйте снова");
            }

            string source = "";

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }

        private string UrlReplacement(string url, string prefix, string template)
        {
            if (url.Contains(prefix))
            {
                int start = url.IndexOf(prefix) + prefix.Length;
                int count = 0;
                for (int i = start; i < url.Length; i++)
                {
                    if (!url[i].IsDigit())
                        break;

                    count++;
                }

                return url.Remove(start, count);
            }

            if (template == "" || template == null)
            {
                return $"{url}{prefix}/";
            }

            template = template.Replace("{BaseURL}", url);
            return template.Replace("{Prefix}", prefix);
        }
    }
}
