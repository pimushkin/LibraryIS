using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryIS.Persistence.Migrations
{
    public partial class AddCopyBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CopyRequest");

            migrationBuilder.CreateTable(
                name: "CopyBook",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopyBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CopyBook_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicCopyRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    PagesRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReaderProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicCopyRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicCopyRequest_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElectronicCopyRequest_ReaderProfile_ReaderProfileId",
                        column: x => x.ReaderProfileId,
                        principalTable: "ReaderProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CopyBook_BookId",
                table: "CopyBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicCopyRequest_BookId",
                table: "ElectronicCopyRequest",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicCopyRequest_ReaderProfileId",
                table: "ElectronicCopyRequest",
                column: "ReaderProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CopyBook");

            migrationBuilder.DropTable(
                name: "ElectronicCopyRequest");

            migrationBuilder.CreateTable(
                name: "CopyRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PagesRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReaderProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopyRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CopyRequest_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CopyRequest_ReaderProfile_ReaderProfileId",
                        column: x => x.ReaderProfileId,
                        principalTable: "ReaderProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CopyRequest_BookId",
                table: "CopyRequest",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CopyRequest_ReaderProfileId",
                table: "CopyRequest",
                column: "ReaderProfileId");
        }
    }
}
