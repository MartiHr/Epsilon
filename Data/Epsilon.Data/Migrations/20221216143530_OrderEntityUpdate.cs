using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epsilon.Data.Migrations
{
    public partial class OrderEntityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Computers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Computers_OrderId",
                table: "Computers",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Orders_OrderId",
                table: "Computers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Orders_OrderId",
                table: "Computers");

            migrationBuilder.DropIndex(
                name: "IX_Computers_OrderId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Computers");
        }
    }
}
