using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarZone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class user_roles_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "HashPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "HashPassword", "Roles" },
                values: new object[] { "mypassword", "[\"User\"]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Roles",
                table: "Users",
                newName: "Password");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "mypassword");
        }
    }
}
