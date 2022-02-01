using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummonersRiftShop.Models
{
    public class Item
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Quality { get; set; }
        public string Effects { get; set; }
        public int Price { get; set; }
        public string Icon { get; set; }

        /*
        // Конструктор в модели?
        public Item(string[] parameters)
        {
            Id = int.Parse(parameters[0]);
            Name = parameters[1];
            Quality = parameters[2];
            Effects = parameters[3];
            Price = int.Parse(parameters[4]);
            Icon = parameters[5];
        }
        */
    }
}
