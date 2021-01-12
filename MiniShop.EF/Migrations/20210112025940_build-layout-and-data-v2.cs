using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.EF.Migrations
{
    public partial class buildlayoutanddatav2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryIds",
                table: "Product");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Product",
                nullable: true);

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

            migrationBuilder.Sql(@"
            truncate table Categorys
            insert into Categorys(Id, CreatedDate, UpdatedDate, CreatedBy, UpdatedBy, [Name], [Description], NotUse) values('2FC89E20-B009-427B-8E34-4483BC0638FA', '2020-11-07 14:08:30.4617815','2020-11-07 14:08:30.4617878','ADMIN','ADMIN', N'Thực phẩm - Mỹ phẩm', N'Thực phẩm chức năng, mỹ phẩm làm đẹp,...',0)

            update Product
            set CategoryId = '2FC89E20-B009-427B-8E34-4483BC0638FA'
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categorys_CategoryId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PriceIgnore",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Categorys");

            migrationBuilder.AddColumn<string>(
                name: "CategoryIds",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
