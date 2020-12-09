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
        public DbSet<Product> Products { get; set; }        
        public DbSet<TouchHistory> TouchHistorys { get; set; }        
        public DbSet<Category> Categorys { get; set; }        
        public DbSet<Area> Areas { get; set; }        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(eb =>
            {
                eb.ToTable("Blog");
                eb.Property(b => b.Content).HasColumnType("ntext");
                eb.Property(b => b.DescriptionShort).HasColumnType("nvarchar(500)");
                eb.Property(b => b.Author).HasColumnType("nvarchar(100)");
                eb.Property(b => b.Category).HasColumnType("nvarchar(200)");
                eb.Property(b => b.HashTag).HasColumnType("nvarchar(200)");
                eb.Property(b => b.PicturePath).HasColumnType("nvarchar(500)");
                eb.Property(b => b.ReadMorePath).HasColumnType("nvarchar(1000)");
                eb.Property(b => b.Title).HasColumnType("nvarchar(250)");

            });
            modelBuilder.ApplyConfiguration(new MiniShopEntityConfiguration());
        }
    }
}
