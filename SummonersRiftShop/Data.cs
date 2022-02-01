using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SummonersRiftShop.Models;
using System.Diagnostics;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using AngleSharp;
using System.IO;

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

                foreach (var itemUrl in itemsUrls)
                    itemsParameters.Add(Parser.GetItem(itemUrl).Result);

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

                List<Category> categories = new List<Category>();
                List<string> categoryNames = items.Select(item => item.Quality).Distinct().ToList();
                foreach (var categoryName in categoryNames)
                {
                    categories.Add(
                        new Category
                        {
                            Name = categoryName
                        }
                    );
                }
                context.Categories.AddRange(categories);
                context.SaveChanges();

                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Items ON;");
                    context.Items.AddRange(items);
                    context.SaveChanges();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Items OFF;");
                    transaction.Commit();
                }

                Debug.WriteLine("Table creating done!");

                //Замена ссылок на картинки в инете на их локальные адресса в таблице
                //UpdateItemIcons(context, items);
            }
        }

        private static void UpdateItemIcons(RiftShopContext context, List<Item> items)
        {
            foreach (var item in items)
            {
                context.Items.Attach(item);
                context.Entry(item).Property(i => i.Icon).IsModified = true;
                context.SaveChanges();
                Debug.WriteLine($"{item.Name} updated");
            }
        }
    }
}