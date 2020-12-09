using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.EF.Migrations
{
    public partial class addblog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 20, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 20, nullable: true),
                    Content = table.Column<string>(type: "ntext", nullable: true),
                    DescriptionShort = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    HashTag = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PicturePath = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    ReadMorePath = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    NotUse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");
        }
    }
}
