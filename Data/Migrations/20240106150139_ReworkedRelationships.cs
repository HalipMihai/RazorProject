using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReworkedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionContracts_Persons_PersonID",
                table: "AdoptionContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_AdoptionContracts_AdoptionContractId",
                table: "Dogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Enclosures_EnclosureId",
                table: "Dogs");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_AdoptionContractId",
                table: "Dogs");

            migrationBuilder.RenameColumn(
                name: "PersonID",
                table: "AdoptionContracts",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionContracts_PersonID",
                table: "AdoptionContracts",
                newName: "IX_AdoptionContracts_PersonId");

            migrationBuilder.AlterColumn<int>(
                name: "EnclosureId",
                table: "Dogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AdoptionContractId",
                table: "Dogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_AdoptionContractId",
                table: "Dogs",
                column: "AdoptionContractId",
                unique: true,
                filter: "[AdoptionContractId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionContracts_Persons_PersonId",
                table: "AdoptionContracts",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_AdoptionContracts_AdoptionContractId",
                table: "Dogs",
                column: "AdoptionContractId",
                principalTable: "AdoptionContracts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Enclosures_EnclosureId",
                table: "Dogs",
                column: "EnclosureId",
                principalTable: "Enclosures",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionContracts_Persons_PersonId",
                table: "AdoptionContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_AdoptionContracts_AdoptionContractId",
                table: "Dogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Enclosures_EnclosureId",
                table: "Dogs");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_AdoptionContractId",
                table: "Dogs");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "AdoptionContracts",
                newName: "PersonID");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionContracts_PersonId",
                table: "AdoptionContracts",
                newName: "IX_AdoptionContracts_PersonID");

            migrationBuilder.AlterColumn<int>(
                name: "EnclosureId",
                table: "Dogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdoptionContractId",
                table: "Dogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_AdoptionContractId",
                table: "Dogs",
                column: "AdoptionContractId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionContracts_Persons_PersonID",
                table: "AdoptionContracts",
                column: "PersonID",
                principalTable: "Persons",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_AdoptionContracts_AdoptionContractId",
                table: "Dogs",
                column: "AdoptionContractId",
                principalTable: "AdoptionContracts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Enclosures_EnclosureId",
                table: "Dogs",
                column: "EnclosureId",
                principalTable: "Enclosures",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
