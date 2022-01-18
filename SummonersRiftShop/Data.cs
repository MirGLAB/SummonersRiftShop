using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SummonersRiftShop.Models;
using System.Diagnostics;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace SummonersRiftShop
{
    public class Data
    {
        public static void Initialize(RiftShopContext context)
        {
            // Eсли данные в таблице Items в бд отсутствуют 
            if (!context.Items.Any())
            {
                List<Item> items = new List<Item>();

                List<string> itemsUrls = new List<string>();
                List<string[]> itemsParameters = new List<string[]>();
                itemsUrls = Parser.GetItemsUrls().Result;

                itemsUrls.Remove("https://leagueoflegends.fandom.com/wiki/Broken_Stopwatch");

                //Debug.WriteLine(itemsUrls.Count);

                foreach (var itemUrl in itemsUrls)
                {
                    //var itemParameters = Parser.GetItem(itemUrl);
                    itemsParameters.Add(Parser.GetItem(itemUrl).Result);
                    //break;
                }

                Debug.WriteLine("Downloading done!");

                foreach (var itemParameters in itemsParameters)
                {
                    items.Add(
                        new Item
                        {
                            Id = int.Parse(itemParameters[0]),
                            Name = itemParameters[1],
                            Quality = itemParameters[2],
                            Effects = itemParameters[3],
                            Price = int.Parse(itemParameters[4]),
                            Icon = itemParameters[5]
                        }
                    );
                }

                /*
                foreach (var item in items)
                {
                    Debug.WriteLine($"Item: {item.Name}");
                    Debug.WriteLine($"ID: {item.Id}");
                    Debug.WriteLine($"Quaity: {item.Quality}");
                    Debug.WriteLine($"Effects: {item.Effects}");
                    Debug.WriteLine($"Price: {item.Price}");
                    Debug.WriteLine($"Icon: {item.Icon}");
                    Debug.Write("\n");
                }
                */

                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Items ON;");
                    context.Items.AddRange(items);
                    context.SaveChanges();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Items OFF;");
                    transaction.Commit();
                }

                Debug.WriteLine("DB creating done!");
            }
        }
    }
}
