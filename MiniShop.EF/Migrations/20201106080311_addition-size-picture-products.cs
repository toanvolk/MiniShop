using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.EF.Migrations
{
    public partial class additionsizepictureproducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BigPicture",
                table: "Product",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmallPicture",
                table: "Product",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BigPicture",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SmallPicture",
                table: "Product");
        }
    }
}
