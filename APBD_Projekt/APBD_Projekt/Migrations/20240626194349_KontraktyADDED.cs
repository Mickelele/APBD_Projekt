using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class KontraktyADDED : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kontrakty",
                columns: table => new
                {
                    KontraktID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    ClientType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataWaznosciOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataWaznosciDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CzyOplacona = table.Column<bool>(type: "bit", nullable: false),
                    CzyAktywna = table.Column<bool>(type: "bit", nullable: false),
                    Cena = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    InformacjaOAktualizacjach = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LataDodatkowegoWsparcia = table.Column<int>(type: "int", nullable: false),
                    ZnizkaProcent = table.Column<int>(type: "int", nullable: false),
                    OprogramowanieWersja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OprogramowanieID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontrakty", x => x.KontraktID);
                    table.ForeignKey(
                        name: "FK_Kontrakty_Oprogramowania_OprogramowanieID",
                        column: x => x.OprogramowanieID,
                        principalTable: "Oprogramowania",
                        principalColumn: "OprogramowanieID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kontrakty_OprogramowanieID",
                table: "Kontrakty",
                column: "OprogramowanieID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kontrakty");
        }
    }
}
