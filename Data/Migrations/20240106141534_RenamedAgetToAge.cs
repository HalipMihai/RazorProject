using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedAgetToAge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Aget",
                table: "Dogs",
                newName: "Age");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Dogs",
                newName: "Aget");
        }
    }
}
