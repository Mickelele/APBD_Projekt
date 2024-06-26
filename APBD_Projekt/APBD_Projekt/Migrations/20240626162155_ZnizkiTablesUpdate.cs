using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class ZnizkiTablesUpdate : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_OprogramowanieZnizka_Znizka_ZnizkiZnikaID",
                table: "OprogramowanieZnizka");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Znizka",
                table: "Znizka");

            migrationBuilder.RenameTable(
                name: "Znizka",
                newName: "Znizki");

            migrationBuilder.AlterColumn<int>(
                name: "KlientFizycznyID",
                table: "Oprogramowania",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FirmaID",
                table: "Oprogramowania",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Znizki",
                table: "Znizki",
                column: "ZnikaID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_OprogramowanieZnizka_Znizki_ZnizkiZnikaID",
                table: "OprogramowanieZnizka",
                column: "ZnizkiZnikaID",
                principalTable: "Znizki",
                principalColumn: "ZnikaID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oprogramowania_Firmy_FirmaID",
                table: "Oprogramowania");

            migrationBuilder.DropForeignKey(
                name: "FK_Oprogramowania_KlienciFizyczni_KlientFizycznyID",
                table: "Oprogramowania");

            migrationBuilder.DropForeignKey(
                name: "FK_OprogramowanieZnizka_Znizki_ZnizkiZnikaID",
                table: "OprogramowanieZnizka");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Znizki",
                table: "Znizki");

            migrationBuilder.RenameTable(
                name: "Znizki",
                newName: "Znizka");

            migrationBuilder.AlterColumn<int>(
                name: "KlientFizycznyID",
                table: "Oprogramowania",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FirmaID",
                table: "Oprogramowania",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Znizka",
                table: "Znizka",
                column: "ZnikaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Oprogramowania_Firmy_FirmaID",
                table: "Oprogramowania",
                column: "FirmaID",
                principalTable: "Firmy",
                principalColumn: "FirmaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Oprogramowania_KlienciFizyczni_KlientFizycznyID",
                table: "Oprogramowania",
                column: "KlientFizycznyID",
                principalTable: "KlienciFizyczni",
                principalColumn: "KlientID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OprogramowanieZnizka_Znizka_ZnizkiZnikaID",
                table: "OprogramowanieZnizka",
                column: "ZnizkiZnikaID",
                principalTable: "Znizka",
                principalColumn: "ZnikaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
