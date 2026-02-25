using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarZone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class car_models_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "ModelId", "BrandId", "ModelName" },
                values: new object[,]
                {
                    { 1, 1, "Series 3" },
                    { 2, 1, "Series 5" },
                    { 3, 1, "X5" },
                    { 4, 2, "A4" },
                    { 5, 2, "A6" },
                    { 6, 2, "Q7" },
                    { 7, 3, "C-Class" },
                    { 8, 3, "E-Class" },
                    { 9, 3, "GLE" },
                    { 10, 4, "Golf" },
                    { 11, 4, "Passat" },
                    { 12, 4, "Tiguan" },
                    { 13, 5, "Corolla" },
                    { 14, 5, "Camry" },
                    { 15, 5, "RAV4" },
                    { 16, 6, "Civic" },
                    { 17, 6, "Accord" },
                    { 18, 6, "CR-V" },
                    { 19, 7, "Focus" },
                    { 20, 7, "Mondeo" },
                    { 21, 7, "Mustang" },
                    { 22, 8, "Astra" },
                    { 23, 8, "Insignia" },
                    { 24, 8, "Corsa" },
                    { 25, 9, "208" },
                    { 26, 9, "308" },
                    { 27, 9, "3008" },
                    { 28, 10, "Clio" },
                    { 29, 10, "Megane" },
                    { 30, 10, "Kadjar" },
                    { 31, 11, "Octavia" },
                    { 32, 11, "Superb" },
                    { 33, 11, "Kodiaq" },
                    { 34, 12, "i30" },
                    { 35, 12, "Tucson" },
                    { 36, 12, "Santa Fe" },
                    { 37, 13, "Ceed" },
                    { 38, 13, "Sportage" },
                    { 39, 13, "Sorento" },
                    { 40, 14, "Mazda 3" },
                    { 41, 14, "Mazda 6" },
                    { 42, 14, "CX-5" },
                    { 43, 15, "Qashqai" },
                    { 44, 15, "X-Trail" },
                    { 45, 15, "Micra" },
                    { 46, 16, "S60" },
                    { 47, 16, "XC60" },
                    { 48, 16, "XC90" },
                    { 49, 17, "Punto" },
                    { 50, 17, "500" },
                    { 51, 17, "Tipo" },
                    { 52, 18, "Giulia" },
                    { 53, 18, "Stelvio" },
                    { 54, 19, "911" },
                    { 55, 19, "Cayenne" },
                    { 56, 20, "Model S" },
                    { 57, 20, "Model 3" },
                    { 58, 20, "Model Y" },
                    { 59, 21, "F8 Tributo" },
                    { 60, 21, "Roma" },
                    { 61, 22, "Huracan" },
                    { 62, 22, "Aventador" },
                    { 63, 23, "Ghibli" },
                    { 64, 23, "Levante" },
                    { 65, 24, "Continental GT" },
                    { 66, 24, "Bentayga" },
                    { 67, 25, "Ghost" },
                    { 68, 25, "Phantom" },
                    { 69, 27, "Atto 3" },
                    { 70, 27, "Seal" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "ModelId",
                keyValue: 70);
        }
    }
}
