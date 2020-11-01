using MiniShop.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.EF
{
    public class MiniShopEntityConfiguration
        : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasData(
                 new Area() { Id = Guid.NewGuid(),Code = "VN", Name = "VIET NAM" },
                 new Area() { Id = Guid.NewGuid(),Code = "TH", Name = "THAILAND" },
                 new Area() { Id = Guid.NewGuid(),Code = "TW", Name = "TAIWAN" },
                 new Area() { Id = Guid.NewGuid(),Code = "SA", Name = "SAUDI ARABIA" },
                 new Area() { Id = Guid.NewGuid(),Code = "RO", Name = "ROMANIA" },
                 new Area() { Id = Guid.NewGuid(),Code = "PT", Name = "PORTUGAL" },
                 new Area() { Id = Guid.NewGuid(),Code = "MY", Name = "MALAYSIA" },
                 new Area() { Id = Guid.NewGuid(),Code = "ID", Name = "INDONESIA" },
                 new Area() { Id = Guid.NewGuid(),Code = "HU", Name = "HUNGARY" },
                 new Area() { Id = Guid.NewGuid(),Code = "HK", Name = "HONG KONG" },
                 new Area() { Id = Guid.NewGuid(),Code = "KH", Name = "CAMBODIA" },
                 new Area() { Id = Guid.NewGuid(),Code = "BG", Name = "BULGARIA" }                 
             );
        }
    }
}
