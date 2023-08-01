using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWorkshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Cart_CartId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_CartId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Services");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarWorkshopServiceCart");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_CartId",
                table: "Services",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Cart_CartId",
                table: "Services",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id");
        }
    }
}
