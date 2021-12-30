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
        ShopContext db;

        public HomeController(ShopContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
