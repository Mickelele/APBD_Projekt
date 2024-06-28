using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class SubskrybjceAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubskrybcjaID",
                table: "Platnosci",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Subskrybcja",
                columns: table => new
                {
                    SubskrybcjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OprogramowanieID = table.Column<int>(type: "int", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CzasOdnowienia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cena = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subskrybcja", x => x.SubskrybcjaID);
                    table.ForeignKey(
                        name: "FK_Subskrybcja_Oprogramowania_OprogramowanieID",
                        column: x => x.OprogramowanieID,
                        principalTable: "Oprogramowania",
                        principalColumn: "OprogramowanieID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Platnosci_SubskrybcjaID",
                table: "Platnosci",
                column: "SubskrybcjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Subskrybcja_OprogramowanieID",
                table: "Subskrybcja",
                column: "OprogramowanieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Platnosci_Subskrybcja_SubskrybcjaID",
                table: "Platnosci",
                column: "SubskrybcjaID",
                principalTable: "Subskrybcja",
                principalColumn: "SubskrybcjaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Platnosci_Subskrybcja_SubskrybcjaID",
                table: "Platnosci");

            migrationBuilder.DropTable(
                name: "Subskrybcja");

            migrationBuilder.DropIndex(
                name: "IX_Platnosci_SubskrybcjaID",
                table: "Platnosci");

            migrationBuilder.DropColumn(
                name: "SubskrybcjaID",
                table: "Platnosci");
        }
    }
}
