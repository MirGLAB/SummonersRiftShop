using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummonersRiftShop.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ViewBag.Items = db.Items.ToList();
            ViewBag.Categories = db.Categories.ToList();
            return View();
            //return View(db.Items.ToList());
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
