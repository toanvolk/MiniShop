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
                 new Area() { Id = new Guid("09432590-1d58-424e-a061-e931f9166ce8"), Code = "VN", Name = "VIET NAM" },
                 new Area() { Id = new Guid("c9c2a259-ef45-4d77-9257-81ae278593d6"), Code = "TH", Name = "THAILAND" },
                 new Area() { Id = new Guid("d308f9d9-a5e5-4dd4-9691-7a57d6165b49"), Code = "TW", Name = "TAIWAN" },
                 new Area() { Id = new Guid("1ad385c8-0b90-41ca-a3ae-a08c7e4ce85d"), Code = "SA", Name = "SAUDI ARABIA" },
                 new Area() { Id = new Guid("a5f919b0-d3fb-4ef8-9a99-34a53ce6ec1b"), Code = "RO", Name = "ROMANIA" },
                 new Area() { Id = new Guid("b5dc81cd-68e2-4aed-bd28-726a0a55e51f"), Code = "PT", Name = "PORTUGAL" },
                 new Area() { Id = new Guid("08a59071-3052-49c4-b4c3-84c6f023ea93"), Code = "MY", Name = "MALAYSIA" },
                 new Area() { Id = new Guid("4cf77c3e-80e4-4017-8df5-ba75103b37d2"), Code = "ID", Name = "INDONESIA" },
                 new Area() { Id = new Guid("2e67069e-5445-4766-8aa4-22eda3ead2b8"), Code = "HU", Name = "HUNGARY" },
                 new Area() { Id = new Guid("a95abdbf-ebc8-4073-b7af-1c84711dad54"), Code = "HK", Name = "HONG KONG" },
                 new Area() { Id = new Guid("4d594520-8076-407c-a2b2-f9ba2a61b1ea"), Code = "KH", Name = "CAMBODIA" },
                 new Area() { Id = new Guid("d2f79b4b-8b5e-4c14-964f-dd95fd72d2ee"), Code = "BG", Name = "BULGARIA" }                 
             );
        }
    }
}
