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
        public string Address { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
