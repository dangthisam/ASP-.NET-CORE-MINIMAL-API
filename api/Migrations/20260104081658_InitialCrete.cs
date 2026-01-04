using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCrete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bookapi");

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "bookapi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalPages = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "bookapi",
                table: "Books",
                columns: new[] { "Id", "Author", "Category", "Description", "Language", "Title", "TotalPages" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Paulo Coelho", "Fiction", "The Alchemist follows the journey of an Andalusian shepherd", "English", "The Alchemist", 208 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Harper Lee", "Fiction", "A novel about the serious issues of rape and racial inequality.", "English", "To Kill a Mockingbird", 281 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "George Orwell", "Fiction", "A dystopian social science fiction novel and cautionary tale about the dangers of totalitarianism. ", "English", "1984", 328 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books",
                schema: "bookapi");
        }
    }
}
