using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using System.Diagnostics;
using AngleSharp.Html.Dom;
using System.Text.RegularExpressions;
using System.Net;

namespace SummonersRiftShop
{
    public class Parser
    {
        public static async Task<List<string>> GetItemsUrls()
        {
            List<string> items = new List<string>();
            var url = "https://leagueoflegends.fandom.com/wiki/Item_(League_of_Legends)";
            var hrefUrl = "https://leagueoflegends.fandom.com";

            using var parsingContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            using var document = await parsingContext.OpenAsync(url);

            var itemArrays = document.QuerySelectorAll(@".tlist").
                Take(9).Where(ils => ils.Index() == 1 || ils.Index() >= 15);
            var qualityTier = 0;

            foreach (var itemArray in itemArrays)
            {
                var itemIcons = itemArray.QuerySelectorAll("a").OfType<IHtmlAnchorElement>();

                foreach (var itemIcon in itemIcons)
                {
                    var itemHref = $"{hrefUrl}{itemIcon.GetAttribute("href")}";
                    items.Add(itemHref);
                }
                qualityTier++;
            }

            Debug.WriteLine("Items array parsing done!");

            return items;
        }

        public static async Task<string[]> GetItem(string itemUrl)
        {
            string[] itemParameters = new string[6];

            using var parsingContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            using var document = await parsingContext.OpenAsync(itemUrl);

            itemParameters[0] = document.QuerySelector(@"td[data-source='id']").TextContent;

            itemParameters[1] = Regex.Match(document.Title, @"[^\|]+").ToString();
            itemParameters[1] = itemParameters[1].Substring(0, itemParameters[1].Length - 1);

            itemParameters[2] = document.QuerySelector("div.page-header__categories").
                QuerySelector("a").TextContent;

            var parsingParameters = document.QuerySelector("aside.portable-infobox").
                QuerySelectorAll("section.pi-item").
                Where(pp => pp.TextContent.Contains("Stats") || 
                pp.TextContent.Contains("Active") ||
                pp.TextContent.Contains("Passive"));

            itemParameters[3] = "";

            foreach(var parsingParameter in parsingParameters)
            {
                var parameters = parsingParameter.QuerySelectorAll("div.pi-data-value");
                foreach(var parameter in parameters)
                    itemParameters[3] += $"{parameter.TextContent}\n";
            }
            itemParameters[3] = itemParameters[3].Trim('\n', ' ');

            itemParameters[4] = document.QuerySelector(@"td[data-source='buy']").TextContent.Trim('\n', ' ');

            try
            {
                itemParameters[5] = document.QuerySelector($"aside[role='region']").
                    QuerySelector($"img[alt=\"{itemParameters[1]}\"]").
                    GetAttribute("src");

                // Загрузка иконок
                using (WebClient client = new WebClient())
                {
                    client.DownloadFileAsync(new Url(itemParameters[5]),
                        @$"C:\\Users\mirgl\source\repos\SummonersRiftShop\SummonersRiftShop\Images\{itemParameters[1]}.png");
                } 
            }
            catch
            {
            }

            return itemParameters;
        }
    }
}
