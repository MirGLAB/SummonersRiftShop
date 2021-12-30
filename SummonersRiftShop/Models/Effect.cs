using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummonersRiftShop.Models
{
    public class Effect
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        //public static List<string> effectTypes = new List<string> { "Stats", "Active", "Passive" };
    }
}
