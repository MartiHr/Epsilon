using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epsilon.Data.Migrations
{
    public partial class AddedOwnerToComputer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Computers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Computers_CreatorId",
                table: "Computers",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_AspNetUsers_CreatorId",
                table: "Computers",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_AspNetUsers_CreatorId",
                table: "Computers");

            migrationBuilder.DropIndex(
                name: "IX_Computers_CreatorId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Computers");
        }
    }
}
