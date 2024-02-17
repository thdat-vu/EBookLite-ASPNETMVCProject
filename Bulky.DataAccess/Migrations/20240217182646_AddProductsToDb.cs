using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Billy Spark", "Lorem", "SWD9999901", 99.0, 90.0, 80.0, 85.0, "Fortune Of Time" },
                    { 2, "Aryan Rand", "Lorem", "SWD9999902", 100.0, 95.0, 95.0, 90.0, "Back To Eden" },
                    { 3, "Jhumpa Lahiri", "the PEN/Hemingway Award, and the highest critical praise for its grace, acuity, and compassion in detailing lives transported from India to America.", "SWD9999903", 115.0, 100.0, 90.0, 95.0, "The Namesake" },
                    { 4, "Suzanne Collins", " Without really meaning to, she becomes a contender. But if she is to win, she will have to start making choices that weight survival against humanity and life against love", "SWD9999904", 200.0, 190.0, 150.0, 180.0, "The Hunger Games" },
                    { 5, "Jane Austen", " And Jane Austen's radiant wit sparkles as her characters dance a delicate quadrille of flirtation and intrigue, making this book the most superb comedy of manners of Regency England.", "9780679783268", 400.0, 350.0, 300.0, 320.0, "Pride and Prejudice" },
                    { 6, "Elle Kennedy", "She's about to make a deal with the college bad boy... Hannah Wells has finally found someone who turns her on. But while she might be confident in every other area of her life, she's carting around a full set of baggage when it comes to sex and seduction.", "9781775293934", 150.0, 140.0, 100.0, 120.0, "The Deal" },
                    { 7, "Gillian Flynn", "These are the questions Nick Dunne finds himself asking on the morning of his fifth wedding anniversary when his wife Amy suddenly disappears. The police suspect Nick.", "9781775293935", 460.0, 420.0, 380.0, 400.0, "Gone Girl" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
