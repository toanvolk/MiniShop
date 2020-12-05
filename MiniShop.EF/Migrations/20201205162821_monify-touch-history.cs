using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.EF.Migrations
{
    public partial class monifytouchhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "TouchHistory");

            migrationBuilder.AddColumn<string>(
                name: "KeyView",
                table: "TouchHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TouchHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyView",
                table: "TouchHistory");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "TouchHistory");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "TouchHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
