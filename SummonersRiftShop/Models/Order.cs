using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummonersRiftShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string User { get; set; } 

        public int ItemId { get; set; }
        public int ItemAmount { get; set; }
        public Item Item { get; set; }
    }
}
