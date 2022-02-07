using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SummonersRiftShop.Models
{
    public class FilterViewModel
    {
        public string SelectedCategory { get; private set; }

        public FilterViewModel(string category)
        {
            SelectedCategory = category;
        }
    }
}
