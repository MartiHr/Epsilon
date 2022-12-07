using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epsilon.Data.Migrations
{
    public partial class ColumnNameUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Editor_CreatorId",
                table: "Computers");

            migrationBuilder.DropForeignKey(
                name: "FK_Editor_AspNetUsers_ApplicationUserId",
                table: "Editor");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Editor_CreatorId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Editor",
                table: "Editor");

            migrationBuilder.RenameTable(
                name: "Editor",
                newName: "Editors");

            migrationBuilder.RenameIndex(
                name: "IX_Editor_IsDeleted",
                table: "Editors",
                newName: "IX_Editors_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Editor_ApplicationUserId",
                table: "Editors",
                newName: "IX_Editors_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Editors",
                table: "Editors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Editors_CreatorId",
                table: "Computers",
                column: "CreatorId",
                principalTable: "Editors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_AspNetUsers_ApplicationUserId",
                table: "Editors",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Editors_CreatorId",
                table: "Images",
                column: "CreatorId",
                principalTable: "Editors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Editors_CreatorId",
                table: "Computers");

            migrationBuilder.DropForeignKey(
                name: "FK_Editors_AspNetUsers_ApplicationUserId",
                table: "Editors");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Editors_CreatorId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Editors",
                table: "Editors");

            migrationBuilder.RenameTable(
                name: "Editors",
                newName: "Editor");

            migrationBuilder.RenameIndex(
                name: "IX_Editors_IsDeleted",
                table: "Editor",
                newName: "IX_Editor_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Editors_ApplicationUserId",
                table: "Editor",
                newName: "IX_Editor_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Editor",
                table: "Editor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Editor_CreatorId",
                table: "Computers",
                column: "CreatorId",
                principalTable: "Editor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Editor_AspNetUsers_ApplicationUserId",
                table: "Editor",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
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
    }
}
