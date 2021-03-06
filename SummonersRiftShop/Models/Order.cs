using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SummonersRiftShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        public string User { get; set; } 
        public string Address { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
