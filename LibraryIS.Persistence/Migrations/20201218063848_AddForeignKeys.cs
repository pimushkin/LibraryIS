using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryIS.Persistence.Migrations
{
    public partial class AddForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicCopyRequest_ReaderProfile_ReaderProfileId",
                table: "ElectronicCopyRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedBook_ReaderProfile_ReaderProfileId",
                table: "ReservedBook");

            migrationBuilder.DropForeignKey(
                name: "FK_TakenBook_ReaderProfile_ReaderProfileId",
                table: "TakenBook");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReaderProfileId",
                table: "TakenBook",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReaderProfileId",
                table: "ReservedBook",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReaderProfileId",
                table: "ElectronicCopyRequest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReaderProfileId",
                table: "CopyBook",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CopyBook_ReaderProfileId",
                table: "CopyBook",
                column: "ReaderProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_CopyBook_ReaderProfile_ReaderProfileId",
                table: "CopyBook",
                column: "ReaderProfileId",
                principalTable: "ReaderProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicCopyRequest_ReaderProfile_ReaderProfileId",
                table: "ElectronicCopyRequest",
                column: "ReaderProfileId",
                principalTable: "ReaderProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedBook_ReaderProfile_ReaderProfileId",
                table: "ReservedBook",
                column: "ReaderProfileId",
                principalTable: "ReaderProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakenBook_ReaderProfile_ReaderProfileId",
                table: "TakenBook",
                column: "ReaderProfileId",
                principalTable: "ReaderProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CopyBook_ReaderProfile_ReaderProfileId",
                table: "CopyBook");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicCopyRequest_ReaderProfile_ReaderProfileId",
                table: "ElectronicCopyRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedBook_ReaderProfile_ReaderProfileId",
                table: "ReservedBook");

            migrationBuilder.DropForeignKey(
                name: "FK_TakenBook_ReaderProfile_ReaderProfileId",
                table: "TakenBook");

            migrationBuilder.DropIndex(
                name: "IX_CopyBook_ReaderProfileId",
                table: "CopyBook");

            migrationBuilder.DropColumn(
                name: "ReaderProfileId",
                table: "CopyBook");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReaderProfileId",
                table: "TakenBook",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReaderProfileId",
                table: "ReservedBook",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReaderProfileId",
                table: "ElectronicCopyRequest",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicCopyRequest_ReaderProfile_ReaderProfileId",
                table: "ElectronicCopyRequest",
                column: "ReaderProfileId",
                principalTable: "ReaderProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedBook_ReaderProfile_ReaderProfileId",
                table: "ReservedBook",
                column: "ReaderProfileId",
                principalTable: "ReaderProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TakenBook_ReaderProfile_ReaderProfileId",
                table: "TakenBook",
                column: "ReaderProfileId",
                principalTable: "ReaderProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
