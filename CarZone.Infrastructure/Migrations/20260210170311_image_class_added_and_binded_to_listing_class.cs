using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarZone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class image_class_added_and_binded_to_listing_class : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Image_Listing_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listing",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ListingId",
                table: "Image",
                column: "ListingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
