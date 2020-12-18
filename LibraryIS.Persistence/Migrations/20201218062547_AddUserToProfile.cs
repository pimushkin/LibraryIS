using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryIS.Persistence.Migrations
{
    public partial class AddUserToProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_ReaderProfile_ReaderProfileId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ReaderProfileId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ReaderProfileId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Book");

            migrationBuilder.AddForeignKey(
                name: "FK_ReaderProfile_User_Id",
                table: "ReaderProfile",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReaderProfile_User_Id",
                table: "ReaderProfile");

            migrationBuilder.AddColumn<Guid>(
                name: "ReaderProfileId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Book",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ReaderProfileId",
                table: "User",
                column: "ReaderProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_ReaderProfile_ReaderProfileId",
                table: "User",
                column: "ReaderProfileId",
                principalTable: "ReaderProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
