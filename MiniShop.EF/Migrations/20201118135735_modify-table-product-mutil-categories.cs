using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.EF.Migrations
{
    public partial class modifytableproductmutilcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "CategoryIds",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryIds",
                table: "Product");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
