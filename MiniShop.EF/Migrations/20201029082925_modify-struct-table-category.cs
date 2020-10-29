using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.EF.Migrations
{
    public partial class modifystructtablecategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackingLink",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categorys",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NotUse",
                table: "Categorys",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackingLink",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "NotUse",
                table: "Categorys");
        }
    }
}
