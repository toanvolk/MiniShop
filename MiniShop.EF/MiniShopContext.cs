using MiniShop.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.EF
{
    public class MiniShopContext : DbContext
    {
        public MiniShopContext(DbContextOptions<MiniShopContext> options) : base(options) { }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new MiniShopEntityConfiguration());
        }
    }
}
