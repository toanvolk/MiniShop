using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.EF.Migrations
{
    public partial class additionfieldareacodeintoproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("31579ad3-012a-4384-8779-5015f4af05df"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("32383e33-ba44-46d8-8d5a-bfea2e39beb6"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("56be5c40-6f74-4541-9ef0-504216c5c2a7"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("5bbcaecc-6bf9-4700-9038-3411d6fbeff8"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("7cba80fb-ea4f-4c49-ba61-2d7685b7b399"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("899eb9b8-b083-41d1-a83f-05b1d05688c7"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("941de86f-0d0a-4442-b601-222a3439e599"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("992f73c4-dac8-4dbf-af0e-d87e466de190"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("af1ae7eb-f563-4bd3-8d63-6e9a246e9312"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("eb3edbe3-53f2-4813-8904-6c9e33673c02"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("f15fcd0c-31ba-468a-9d64-193423c4c3fe"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("f949f1f2-2fde-48b7-a6e6-082ed04efaf1"));

            migrationBuilder.AddColumn<string>(
                name: "AreaCode",
                table: "Product",
                maxLength: 50,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Name", "ParentId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("09432590-1d58-424e-a061-e931f9166ce8"), "VN", null, null, "VIET NAM", null, null, null },
                    { new Guid("c9c2a259-ef45-4d77-9257-81ae278593d6"), "TH", null, null, "THAILAND", null, null, null },
                    { new Guid("d308f9d9-a5e5-4dd4-9691-7a57d6165b49"), "TW", null, null, "TAIWAN", null, null, null },
                    { new Guid("1ad385c8-0b90-41ca-a3ae-a08c7e4ce85d"), "SA", null, null, "SAUDI ARABIA", null, null, null },
                    { new Guid("a5f919b0-d3fb-4ef8-9a99-34a53ce6ec1b"), "RO", null, null, "ROMANIA", null, null, null },
                    { new Guid("b5dc81cd-68e2-4aed-bd28-726a0a55e51f"), "PT", null, null, "PORTUGAL", null, null, null },
                    { new Guid("08a59071-3052-49c4-b4c3-84c6f023ea93"), "MY", null, null, "MALAYSIA", null, null, null },
                    { new Guid("4cf77c3e-80e4-4017-8df5-ba75103b37d2"), "ID", null, null, "INDONESIA", null, null, null },
                    { new Guid("2e67069e-5445-4766-8aa4-22eda3ead2b8"), "HU", null, null, "HUNGARY", null, null, null },
                    { new Guid("a95abdbf-ebc8-4073-b7af-1c84711dad54"), "HK", null, null, "HONG KONG", null, null, null },
                    { new Guid("4d594520-8076-407c-a2b2-f9ba2a61b1ea"), "KH", null, null, "CAMBODIA", null, null, null },
                    { new Guid("d2f79b4b-8b5e-4c14-964f-dd95fd72d2ee"), "BG", null, null, "BULGARIA", null, null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("08a59071-3052-49c4-b4c3-84c6f023ea93"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("09432590-1d58-424e-a061-e931f9166ce8"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("1ad385c8-0b90-41ca-a3ae-a08c7e4ce85d"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("2e67069e-5445-4766-8aa4-22eda3ead2b8"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("4cf77c3e-80e4-4017-8df5-ba75103b37d2"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("4d594520-8076-407c-a2b2-f9ba2a61b1ea"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("a5f919b0-d3fb-4ef8-9a99-34a53ce6ec1b"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("a95abdbf-ebc8-4073-b7af-1c84711dad54"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("b5dc81cd-68e2-4aed-bd28-726a0a55e51f"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("c9c2a259-ef45-4d77-9257-81ae278593d6"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("d2f79b4b-8b5e-4c14-964f-dd95fd72d2ee"));

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyValue: new Guid("d308f9d9-a5e5-4dd4-9691-7a57d6165b49"));

            migrationBuilder.DropColumn(
                name: "AreaCode",
                table: "Product");

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
    }
}
