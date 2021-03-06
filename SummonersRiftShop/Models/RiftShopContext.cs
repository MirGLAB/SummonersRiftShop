using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SummonersRiftShop.Models
{
    public class RiftShopContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public RiftShopContext(DbContextOptions<RiftShopContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
