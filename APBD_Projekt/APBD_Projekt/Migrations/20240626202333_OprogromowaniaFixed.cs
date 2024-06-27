using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class OprogromowaniaFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oprogramowania_Firmy_FirmaID",
                table: "Oprogramowania");

            migrationBuilder.DropForeignKey(
                name: "FK_Oprogramowania_KlienciFizyczni_KlientFizycznyID",
                table: "Oprogramowania");

            migrationBuilder.DropIndex(
                name: "IX_Oprogramowania_FirmaID",
                table: "Oprogramowania");

            migrationBuilder.DropIndex(
                name: "IX_Oprogramowania_KlientFizycznyID",
                table: "Oprogramowania");

            migrationBuilder.DropColumn(
                name: "FirmaID",
                table: "Oprogramowania");

            migrationBuilder.DropColumn(
                name: "KlientFizycznyID",
                table: "Oprogramowania");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirmaID",
                table: "Oprogramowania",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KlientFizycznyID",
                table: "Oprogramowania",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oprogramowania_FirmaID",
                table: "Oprogramowania",
                column: "FirmaID");

            migrationBuilder.CreateIndex(
                name: "IX_Oprogramowania_KlientFizycznyID",
                table: "Oprogramowania",
                column: "KlientFizycznyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Oprogramowania_Firmy_FirmaID",
                table: "Oprogramowania",
                column: "FirmaID",
                principalTable: "Firmy",
                principalColumn: "FirmaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Oprogramowania_KlienciFizyczni_KlientFizycznyID",
                table: "Oprogramowania",
                column: "KlientFizycznyID",
                principalTable: "KlienciFizyczni",
                principalColumn: "KlientID");
        }
    }
}
