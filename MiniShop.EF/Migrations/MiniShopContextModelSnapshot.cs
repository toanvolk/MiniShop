﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniShop.EF;

namespace MiniShop.EF.Migrations
{
    [DbContext(typeof(MiniShopContext))]
    partial class MiniShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MiniShop.EF.Area", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Area");

                    b.HasData(
                        new
                        {
                            Id = new Guid("09432590-1d58-424e-a061-e931f9166ce8"),
                            Code = "VN",
                            Name = "VIET NAM"
                        },
                        new
                        {
                            Id = new Guid("c9c2a259-ef45-4d77-9257-81ae278593d6"),
                            Code = "TH",
                            Name = "THAILAND"
                        },
                        new
                        {
                            Id = new Guid("d308f9d9-a5e5-4dd4-9691-7a57d6165b49"),
                            Code = "TW",
                            Name = "TAIWAN"
                        },
                        new
                        {
                            Id = new Guid("1ad385c8-0b90-41ca-a3ae-a08c7e4ce85d"),
                            Code = "SA",
                            Name = "SAUDI ARABIA"
                        },
                        new
                        {
                            Id = new Guid("a5f919b0-d3fb-4ef8-9a99-34a53ce6ec1b"),
                            Code = "RO",
                            Name = "ROMANIA"
                        },
                        new
                        {
                            Id = new Guid("b5dc81cd-68e2-4aed-bd28-726a0a55e51f"),
                            Code = "PT",
                            Name = "PORTUGAL"
                        },
                        new
                        {
                            Id = new Guid("08a59071-3052-49c4-b4c3-84c6f023ea93"),
                            Code = "MY",
                            Name = "MALAYSIA"
                        },
                        new
                        {
                            Id = new Guid("4cf77c3e-80e4-4017-8df5-ba75103b37d2"),
                            Code = "ID",
                            Name = "INDONESIA"
                        },
                        new
                        {
                            Id = new Guid("2e67069e-5445-4766-8aa4-22eda3ead2b8"),
                            Code = "HU",
                            Name = "HUNGARY"
                        },
                        new
                        {
                            Id = new Guid("a95abdbf-ebc8-4073-b7af-1c84711dad54"),
                            Code = "HK",
                            Name = "HONG KONG"
                        },
                        new
                        {
                            Id = new Guid("4d594520-8076-407c-a2b2-f9ba2a61b1ea"),
                            Code = "KH",
                            Name = "CAMBODIA"
                        },
                        new
                        {
                            Id = new Guid("d2f79b4b-8b5e-4c14-964f-dd95fd72d2ee"),
                            Code = "BG",
                            Name = "BULGARIA"
                        });
                });

            modelBuilder.Entity("MiniShop.EF.Blog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Content")
                        .HasColumnType("ntext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescriptionShort")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("HashTag")
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("NotUse")
                        .HasColumnType("bit");

                    b.Property<string>("PicturePath")
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReadMorePath")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Blog");
                });

            modelBuilder.Entity("MiniShop.EF.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("NotUse")
                        .HasColumnType("bit");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categorys");
                });

            modelBuilder.Entity("MiniShop.EF.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FontName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("FontSign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("PostType")
                        .HasColumnType("tinyint");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("MiniShop.EF.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AreaCode")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("BigPicture")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("CategoryIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHero")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("NotUse")
                        .HasColumnType("bit");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(12, 2)");

                    b.Property<string>("SmallPicture")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("Tag")
                        .HasColumnType("int");

                    b.Property<string>("TrackingLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("MiniShop.EF.TouchHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("KeyView")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserHostAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TouchHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
