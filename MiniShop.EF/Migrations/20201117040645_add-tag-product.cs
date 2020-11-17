using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.EF.Migrations
{
    public partial class addtagproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tag",
                table: "Product",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Product");
        }
    }
}
