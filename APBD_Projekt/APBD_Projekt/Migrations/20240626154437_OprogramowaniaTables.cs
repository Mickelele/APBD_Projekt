using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class OprogramowaniaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oprogramowania",
                columns: table => new
                {
                    OprogramowanieID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Wersja = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Kategoria = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    KlientFizycznyID = table.Column<int>(type: "int", nullable: false),
                    FirmaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oprogramowania", x => x.OprogramowanieID);
                    table.ForeignKey(
                        name: "FK_Oprogramowania_Firmy_FirmaID",
                        column: x => x.FirmaID,
                        principalTable: "Firmy",
                        principalColumn: "FirmaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oprogramowania_KlienciFizyczni_KlientFizycznyID",
                        column: x => x.KlientFizycznyID,
                        principalTable: "KlienciFizyczni",
                        principalColumn: "KlientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Oprogramowania_FirmaID",
                table: "Oprogramowania",
                column: "FirmaID");

            migrationBuilder.CreateIndex(
                name: "IX_Oprogramowania_KlientFizycznyID",
                table: "Oprogramowania",
                column: "KlientFizycznyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oprogramowania");
        }
    }
}
