using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SummonersRiftShop.Models;
using System.Diagnostics;
using System.Threading;

namespace SummonersRiftShop
{
    public class Data
    {
        public static void Initialize(ShopContext context)
        {
            if (!context.Items.Any())
            {
                List<Item> items = new List<Item>();

                try
                {
                    List<string> itemsUrls = new List<string>();
                    List<string[]> itemsParameters = new List<string[]>();
                    itemsUrls = Parser.GetItemsUrls().Result;

                    itemsUrls.Remove("https://leagueoflegends.fandom.com/wiki/Broken_Stopwatch");

                    //Debug.WriteLine(itemsUrls.Count);

                    foreach (var itemUrl in itemsUrls)
                    {
                        //var itemParameters = Parser.GetItem(itemUrl);
                        itemsParameters.Add(Parser.GetItem(itemUrl).Result);
                    }

                    foreach (var itemParameters in itemsParameters)
                    {
                        for (int i = 0; i < itemParameters.Length; i++)
                            Debug.WriteLine($"{i}) {itemParameters[i]}");
                        Debug.WriteLine("\n");
                    }

                    Debug.WriteLine("Downloading done");
                }
                finally
                {
                    context.SaveChanges();
                }
            }
        }
    }
}
