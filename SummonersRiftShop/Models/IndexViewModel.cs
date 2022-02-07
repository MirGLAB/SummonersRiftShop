using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummonersRiftShop.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Item> Items { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
