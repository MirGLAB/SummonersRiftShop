using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummonersRiftShop.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quality { get; set; }
        public string Effects { get; set; }
        public int Price { get; set; }
        public string Icon { get; set; }
    }
}
