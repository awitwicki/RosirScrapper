using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosirScrapper
{
    public static class HtmlScrapper
    {
        public static async Task<string> GetTopOfTheDayPhotoUrl(string url)
        {
            // Request page
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = await web.LoadFromWebAsync(url);

            // Get data
            HtmlNode data = htmlDoc
                .DocumentNode
                .SelectSingleNode(@"//h4[contains(@class, 'mb-0')]");

            if (data != null)
            {
                //Obecnie na obiekcie przebywa: 2250 osób
                string result = data.InnerText.Replace("Obecnie na obiekcie przebywa: ", "").Replace(" osób", "");

                return result;
            }

            return null;
        }
    }
}
