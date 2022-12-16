using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epsilon.Data.Migrations
{
    public partial class EntitiesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Carts_CartId",
                table: "Computers");

            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Orders_OrderId",
                table: "Computers");

            migrationBuilder.DropIndex(
                name: "IX_Computers_CartId",
                table: "Computers");

            migrationBuilder.DropIndex(
                name: "IX_Computers_OrderId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Computers");

            migrationBuilder.CreateTable(
                name: "CartComputer",
                columns: table => new
                {
                    CartsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ComputersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartComputer", x => new { x.CartsId, x.ComputersId });
                    table.ForeignKey(
                        name: "FK_CartComputer_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartComputer_Computers_ComputersId",
                        column: x => x.ComputersId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComputerOrder",
                columns: table => new
                {
                    ComputersId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerOrder", x => new { x.ComputersId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_ComputerOrder_Computers_ComputersId",
                        column: x => x.ComputersId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComputerOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartComputer_ComputersId",
                table: "CartComputer",
                column: "ComputersId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerOrder_OrdersId",
                table: "ComputerOrder",
                column: "OrdersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartComputer");

            migrationBuilder.DropTable(
                name: "ComputerOrder");

            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "Computers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Computers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Computers_CartId",
                table: "Computers",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_OrderId",
                table: "Computers",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Carts_CartId",
                table: "Computers",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Orders_OrderId",
                table: "Computers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
