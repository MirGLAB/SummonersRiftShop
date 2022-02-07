using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummonersRiftShop.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace SummonersRiftShop.Shop.Controllers
{
    public class HomeController : Controller
    {
        RiftShopContext db;

        public HomeController(RiftShopContext context)
        {
            db = context;
        }

        public IActionResult GetRecipe(string itemName)
        {
            return Content($"{itemName}");
        }

        public async Task<IActionResult> Index(string category = "All items", string name = "",
            int page = 1,
            SortState sortOrder = SortState.IdAsc)
        {
            ViewBag.Categories = db.Categories.ToList();

            int pageSize = 5;

            //фильтрация
            IQueryable<Item> items = db.Items;

            if (category != "All items")
            {
                items = items.Where(i => i.Quality == category);
            }
            if (!String.IsNullOrEmpty(name))
            {
                items = items.Where(i => i.Name.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.IdDesc:
                    items = items.OrderByDescending(i => i.Id);
                    break;
                case SortState.NameAsc:
                    items = items.OrderBy(i => i.Name);
                    break;
                case SortState.NameDesc:
                    items = items.OrderByDescending(i => i.Name);
                    break;
                case SortState.PriceAsc:
                    items = items.OrderBy(i => i.Price);
                    break;
                case SortState.PriceDesc:
                    items = items.OrderByDescending(i => i.Price);
                    break;
                default:
                    items = items.OrderBy(i => i.Id);
                    break;
            }

            // пагинация
            var count = await items.CountAsync();
            var itemsPortion = await items.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(category, name),
                Items = itemsPortion
            };

            ViewBag.Categories = db.Categories.ToList();
            return View(viewModel);
        }




        [HttpGet]
        public async Task<IActionResult> Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            if (id != null)
            {
                Item item = await db.Items.FirstOrDefaultAsync(i => i.Id == id);
                if (item != null)
                    return View(item);
            }
            return NotFound();
        }

        [HttpPost]
        public string Buy(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return $"Thank you {order.User} for the purchase!";
        }
    }
}
