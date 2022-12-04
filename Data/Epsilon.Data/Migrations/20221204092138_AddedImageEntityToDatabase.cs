using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epsilon.Data.Migrations
{
    public partial class AddedImageEntityToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Computers_ComputerId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Editor_CreatorId",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_Image_IsDeleted",
                table: "Images",
                newName: "IX_Images_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Image_CreatorId",
                table: "Images",
                newName: "IX_Images_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Image_ComputerId",
                table: "Images",
                newName: "IX_Images_ComputerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Computers_ComputerId",
                table: "Images",
                column: "ComputerId",
                principalTable: "Computers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Editor_CreatorId",
                table: "Images",
                column: "CreatorId",
                principalTable: "Editor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Computers_ComputerId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Editor_CreatorId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_Images_IsDeleted",
                table: "Image",
                newName: "IX_Image_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Images_CreatorId",
                table: "Image",
                newName: "IX_Image_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ComputerId",
                table: "Image",
                newName: "IX_Image_ComputerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Computers_ComputerId",
                table: "Image",
                column: "ComputerId",
                principalTable: "Computers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Editor_CreatorId",
                table: "Image",
                column: "CreatorId",
                principalTable: "Editor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
