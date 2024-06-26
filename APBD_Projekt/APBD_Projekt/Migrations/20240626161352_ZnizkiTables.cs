using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class ZnizkiTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Cena",
                table: "Oprogramowania",
                type: "float(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Znizka",
                columns: table => new
                {
                    ZnikaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oferta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Wartosc = table.Column<int>(type: "int", nullable: false),
                    ObowiazujeOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObowiazujeDo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Znizka", x => x.ZnikaID);
                });

            migrationBuilder.CreateTable(
                name: "OprogramowanieZnizka",
                columns: table => new
                {
                    OprogramowaniaOprogramowanieID = table.Column<int>(type: "int", nullable: false),
                    ZnizkiZnikaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OprogramowanieZnizka", x => new { x.OprogramowaniaOprogramowanieID, x.ZnizkiZnikaID });
                    table.ForeignKey(
                        name: "FK_OprogramowanieZnizka_Oprogramowania_OprogramowaniaOprogramowanieID",
                        column: x => x.OprogramowaniaOprogramowanieID,
                        principalTable: "Oprogramowania",
                        principalColumn: "OprogramowanieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OprogramowanieZnizka_Znizka_ZnizkiZnikaID",
                        column: x => x.ZnizkiZnikaID,
                        principalTable: "Znizka",
                        principalColumn: "ZnikaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OprogramowanieZnizka_ZnizkiZnikaID",
                table: "OprogramowanieZnizka",
                column: "ZnizkiZnikaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OprogramowanieZnizka");

            migrationBuilder.DropTable(
                name: "Znizka");

            migrationBuilder.DropColumn(
                name: "Cena",
                table: "Oprogramowania");
        }
    }
}
