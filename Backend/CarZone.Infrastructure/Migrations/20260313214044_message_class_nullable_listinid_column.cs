using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarZone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class message_class_nullable_listinid_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Listing_ListingId",
                table: "Message");

            migrationBuilder.AlterColumn<int>(
                name: "ListingId",
                table: "Message",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Listing_ListingId",
                table: "Message",
                column: "ListingId",
                principalTable: "Listing",
                principalColumn: "ListingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Listing_ListingId",
                table: "Message");

            migrationBuilder.AlterColumn<int>(
                name: "ListingId",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Listing_ListingId",
                table: "Message",
                column: "ListingId",
                principalTable: "Listing",
                principalColumn: "ListingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
