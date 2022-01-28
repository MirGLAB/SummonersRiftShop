using Microsoft.AspNetCore.Mvc;
using SummonersRiftShop.Models;
using System.Linq;

namespace SummonersRiftShop.Shop.Controllers
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

            // Запрос к бд итем по ид
            // ViewBag.Item = new Item(...);
            // или return View(item);

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
