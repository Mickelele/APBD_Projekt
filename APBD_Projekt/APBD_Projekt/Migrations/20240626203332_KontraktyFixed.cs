using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class KontraktyFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LataDodatkowegoWsparcia",
                table: "Kontrakty",
                newName: "LataWsparcia");

            migrationBuilder.RenameColumn(
                name: "CzyOplacona",
                table: "Kontrakty",
                newName: "CzyPodpisana");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LataWsparcia",
                table: "Kontrakty",
                newName: "LataDodatkowegoWsparcia");

            migrationBuilder.RenameColumn(
                name: "CzyPodpisana",
                table: "Kontrakty",
                newName: "CzyOplacona");
        }
    }
}
