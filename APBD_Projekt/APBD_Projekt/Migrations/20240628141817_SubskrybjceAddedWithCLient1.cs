using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class SubskrybjceAddedWithCLient1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Platnosci_Subskrybcja_SubskrybcjaID",
                table: "Platnosci");

            migrationBuilder.DropForeignKey(
                name: "FK_Subskrybcja_Oprogramowania_OprogramowanieID",
                table: "Subskrybcja");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subskrybcja",
                table: "Subskrybcja");

            migrationBuilder.RenameTable(
                name: "Subskrybcja",
                newName: "Subskrybcje");

            migrationBuilder.RenameColumn(
                name: "SubskrybcjaID",
                table: "Platnosci",
                newName: "SubskrybjcaID");

            migrationBuilder.RenameIndex(
                name: "IX_Platnosci_SubskrybcjaID",
                table: "Platnosci",
                newName: "IX_Platnosci_SubskrybjcaID");

            migrationBuilder.RenameIndex(
                name: "IX_Subskrybcja_OprogramowanieID",
                table: "Subskrybcje",
                newName: "IX_Subskrybcje_OprogramowanieID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subskrybcje",
                table: "Subskrybcje",
                column: "SubskrybcjaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Platnosci_Subskrybcje_SubskrybjcaID",
                table: "Platnosci",
                column: "SubskrybjcaID",
                principalTable: "Subskrybcje",
                principalColumn: "SubskrybcjaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subskrybcje_Oprogramowania_OprogramowanieID",
                table: "Subskrybcje",
                column: "OprogramowanieID",
                principalTable: "Oprogramowania",
                principalColumn: "OprogramowanieID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Platnosci_Subskrybcje_SubskrybjcaID",
                table: "Platnosci");

            migrationBuilder.DropForeignKey(
                name: "FK_Subskrybcje_Oprogramowania_OprogramowanieID",
                table: "Subskrybcje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subskrybcje",
                table: "Subskrybcje");

            migrationBuilder.RenameTable(
                name: "Subskrybcje",
                newName: "Subskrybcja");

            migrationBuilder.RenameColumn(
                name: "SubskrybjcaID",
                table: "Platnosci",
                newName: "SubskrybcjaID");

            migrationBuilder.RenameIndex(
                name: "IX_Platnosci_SubskrybjcaID",
                table: "Platnosci",
                newName: "IX_Platnosci_SubskrybcjaID");

            migrationBuilder.RenameIndex(
                name: "IX_Subskrybcje_OprogramowanieID",
                table: "Subskrybcja",
                newName: "IX_Subskrybcja_OprogramowanieID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subskrybcja",
                table: "Subskrybcja",
                column: "SubskrybcjaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Platnosci_Subskrybcja_SubskrybcjaID",
                table: "Platnosci",
                column: "SubskrybcjaID",
                principalTable: "Subskrybcja",
                principalColumn: "SubskrybcjaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subskrybcja_Oprogramowania_OprogramowanieID",
                table: "Subskrybcja",
                column: "OprogramowanieID",
                principalTable: "Oprogramowania",
                principalColumn: "OprogramowanieID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
