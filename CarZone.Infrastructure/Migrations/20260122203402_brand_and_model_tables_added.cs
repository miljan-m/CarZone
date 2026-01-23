using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarZone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class brand_and_model_tables_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.ModelId);
                    table.ForeignKey(
                        name: "FK_Model_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "BrandId", "BrandName" },
                values: new object[,]
                {
                    { 1, "BMW" },
                    { 2, "Audi" },
                    { 3, "Mercedes-Benz" },
                    { 4, "Volkswagen" },
                    { 5, "Toyota" },
                    { 6, "Honda" },
                    { 7, "Ford" },
                    { 8, "Opel" },
                    { 9, "Peugeot" },
                    { 10, "Renault" },
                    { 11, "Skoda" },
                    { 12, "Hyundai" },
                    { 13, "Kia" },
                    { 14, "Mazda" },
                    { 15, "Nissan" },
                    { 16, "Volvo" },
                    { 17, "Fiat" },
                    { 18, "Alfa Romeo" },
                    { 19, "Porsche" },
                    { 20, "Tesla" },
                    { 21, "Ferrari" },
                    { 22, "Lamborghini" },
                    { 23, "Maserati" },
                    { 24, "Bentley" },
                    { 25, "Rolls-Royce" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Model_BrandId",
                table: "Model",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Model");

            migrationBuilder.DropTable(
                name: "Brand");
        }
    }
}
