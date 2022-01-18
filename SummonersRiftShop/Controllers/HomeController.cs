using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SummonersRiftShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SummonersRiftShop.Controllers
{
    public class HomeController : Controller
    {
        RiftShopContext db;

        public HomeController(RiftShopContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Items.ToList());
        }

        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.ItemId = id;
            return View();
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
