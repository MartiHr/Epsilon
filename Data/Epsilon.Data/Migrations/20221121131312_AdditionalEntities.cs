using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epsilon.Data.Migrations
{
    public partial class AdditionalEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Manufacturers_ManufacturerId",
                table: "Computers");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Manufacturers_ManufacturerId",
                table: "Parts");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Computers",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Computers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Computers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Computers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Computers_CategoryId",
                table: "Computers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryId",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_IsDeleted",
                table: "Category",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Category_CategoryId",
                table: "Computers",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Manufacturers_ManufacturerId",
                table: "Computers",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Manufacturers_ManufacturerId",
                table: "Parts",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Category_CategoryId",
                table: "Computers");

            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Manufacturers_ManufacturerId",
                table: "Computers");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Manufacturers_ManufacturerId",
                table: "Parts");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Computers_CategoryId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Computers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Computers",
                newName: "Type");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Parts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Computers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Manufacturers_ManufacturerId",
                table: "Computers",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Manufacturers_ManufacturerId",
                table: "Parts",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id");
        }
    }
}
