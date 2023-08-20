using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWorkshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllCartSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceCarts");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.CreateTable(
                name: "CartServices",
                columns: table => new
                {
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarWorkshopServiceId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartServices", x => new { x.AddedById, x.CarWorkshopServiceId });
                    table.ForeignKey(
                        name: "FK_CartServices_AspNetUsers_AddedById",
                        column: x => x.AddedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartServices_Services_CarWorkshopServiceId",
                        column: x => x.CarWorkshopServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartServices_CarWorkshopServiceId",
                table: "CartServices",
                column: "CarWorkshopServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartServices");

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_AspNetUsers_AddedById",
                        column: x => x.AddedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCarts",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false),
                    CarWorkshopServiceId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCarts", x => new { x.CartId, x.CarWorkshopServiceId });
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
                name: "IX_Cart_AddedById",
                table: "Cart",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCarts_CarWorkshopServiceId",
                table: "ServiceCarts",
                column: "CarWorkshopServiceId");
        }
    }
}
