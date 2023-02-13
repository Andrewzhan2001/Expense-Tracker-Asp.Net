using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseRecorder.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NetCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "NetTransactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetTransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_NetTransactions_NetCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "NetCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NetTransactions_CategoryId",
                table: "NetTransactions",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NetTransactions");

            migrationBuilder.DropTable(
                name: "NetCategories");
        }
    }
}
