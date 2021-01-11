using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.EF.Migrations
{
    public partial class rebuildlayoutv21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceIgnore",
                table: "Product",
                type: "decimal(12, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Categorys",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 20, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 20, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    UserHostAddress = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropColumn(
                name: "PriceIgnore",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Categorys");
        }
    }
}
