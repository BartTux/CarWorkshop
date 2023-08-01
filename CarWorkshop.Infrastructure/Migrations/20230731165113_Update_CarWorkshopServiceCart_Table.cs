using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWorkshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_CarWorkshopServiceCart_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceCarts",
                table: "ServiceCarts");

            migrationBuilder.DropColumn(
                name: "AddedAt",
                table: "ServiceCarts");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ServiceCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceCarts",
                table: "ServiceCarts",
                columns: new[] { "CartId", "CarWorkshopServiceId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceCarts",
                table: "ServiceCarts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ServiceCarts");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                table: "ServiceCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceCarts",
                table: "ServiceCarts",
                columns: new[] { "CartId", "CarWorkshopServiceId", "AddedAt" });
        }
    }
}
