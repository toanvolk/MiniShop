using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.EF.Migrations
{
    public partial class adddataarea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TouchHistory",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Product",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categorys",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 20, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 20, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "ParentId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("941de86f-0d0a-4442-b601-222a3439e599"), "VN", null, null, "VIET NAM", null, null, null },
                    { new Guid("31579ad3-012a-4384-8779-5015f4af05df"), "TH", null, null, "THAILAND", null, null, null },
                    { new Guid("899eb9b8-b083-41d1-a83f-05b1d05688c7"), "TW", null, null, "TAIWAN", null, null, null },
                    { new Guid("af1ae7eb-f563-4bd3-8d63-6e9a246e9312"), "SA", null, null, "SAUDI ARABIA", null, null, null },
                    { new Guid("5bbcaecc-6bf9-4700-9038-3411d6fbeff8"), "RO", null, null, "ROMANIA", null, null, null },
                    { new Guid("f949f1f2-2fde-48b7-a6e6-082ed04efaf1"), "PT", null, null, "PORTUGAL", null, null, null },
                    { new Guid("992f73c4-dac8-4dbf-af0e-d87e466de190"), "MY", null, null, "MALAYSIA", null, null, null },
                    { new Guid("56be5c40-6f74-4541-9ef0-504216c5c2a7"), "ID", null, null, "INDONESIA", null, null, null },
                    { new Guid("f15fcd0c-31ba-468a-9d64-193423c4c3fe"), "HU", null, null, "HUNGARY", null, null, null },
                    { new Guid("7cba80fb-ea4f-4c49-ba61-2d7685b7b399"), "HK", null, null, "HONG KONG", null, null, null },
                    { new Guid("eb3edbe3-53f2-4813-8904-6c9e33673c02"), "KH", null, null, "CAMBODIA", null, null, null },
                    { new Guid("32383e33-ba44-46d8-8d5a-bfea2e39beb6"), "BG", null, null, "BULGARIA", null, null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TouchHistory",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categorys",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
