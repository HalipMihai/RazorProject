using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameToEnclosure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Enclosures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Enclosures");
        }
    }
}
