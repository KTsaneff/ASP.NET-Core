using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RESTful_API_Development_dotNET_Eight.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shirts",
                columns: table => new
                {
                    ShirtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shirts", x => x.ShirtId);
                });

            migrationBuilder.InsertData(
                table: "Shirts",
                columns: new[] { "ShirtId", "Brand", "Color", "Gender", "Price", "Size" },
                values: new object[,]
                {
                    { 1, "Nike", "Blue", "Male", 20.0, 10 },
                    { 2, "Adidas", "Red", "Female", 25.0, 7 },
                    { 3, "Puma", "Green", "Male", 30.0, 12 },
                    { 4, "Reebok", "White", "Male", 15.0, 9 },
                    { 5, "Under Armour", "Purple", "Female", 22.0, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shirts");
        }
    }
}
