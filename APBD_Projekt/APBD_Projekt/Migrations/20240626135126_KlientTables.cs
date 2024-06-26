using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class KlientTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Firmy",
                columns: table => new
                {
                    FirmaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaFirmy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NrTelefonu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    KRS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmy", x => x.FirmaID);
                });

            migrationBuilder.CreateTable(
                name: "KlienciFizyczni",
                columns: table => new
                {
                    KlientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NrTelefonu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    czyUsuniety = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlienciFizyczni", x => x.KlientID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Firmy");

            migrationBuilder.DropTable(
                name: "KlienciFizyczni");
        }
    }
}
