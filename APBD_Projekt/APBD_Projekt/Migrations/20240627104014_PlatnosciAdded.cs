using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class PlatnosciAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Platnosci",
                columns: table => new
                {
                    PlatnoscID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IleZaplacono = table.Column<double>(type: "float", nullable: false),
                    PozostaloDoZaplaty = table.Column<double>(type: "float", nullable: false),
                    KontraktID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platnosci", x => x.PlatnoscID);
                    table.ForeignKey(
                        name: "FK_Platnosci_Kontrakty_KontraktID",
                        column: x => x.KontraktID,
                        principalTable: "Kontrakty",
                        principalColumn: "KontraktID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Platnosci_KontraktID",
                table: "Platnosci",
                column: "KontraktID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Platnosci");
        }
    }
}
