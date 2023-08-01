using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWorkshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_CarWorkshopServiceCart_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarWorkshopServiceCart");

            migrationBuilder.DropColumn(
                name: "AddedAt",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Cart");

            migrationBuilder.CreateTable(
                name: "ServiceCarts",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false),
                    CarWorkshopServiceId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCarts", x => new { x.CartId, x.CarWorkshopServiceId, x.AddedAt });
                    table.ForeignKey(
                        name: "FK_ServiceCarts_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceCarts_Services_CarWorkshopServiceId",
                        column: x => x.CarWorkshopServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCarts_CarWorkshopServiceId",
                table: "ServiceCarts",
                column: "CarWorkshopServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceCarts");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                table: "Cart",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CarWorkshopServiceCart",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWorkshopServiceCart", x => new { x.CartId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_CarWorkshopServiceCart_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarWorkshopServiceCart_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarWorkshopServiceCart_ServicesId",
                table: "CarWorkshopServiceCart",
                column: "ServicesId");
        }
    }
}
