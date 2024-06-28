using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class SubskrybjceAddedWithCLient2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Platnosci_Subskrybcje_SubskrybjcaID",
                table: "Platnosci");

            migrationBuilder.DropIndex(
                name: "IX_Platnosci_SubskrybjcaID",
                table: "Platnosci");

            migrationBuilder.DropColumn(
                name: "SubskrybjcaID",
                table: "Platnosci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubskrybjcaID",
                table: "Platnosci",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Platnosci_SubskrybjcaID",
                table: "Platnosci",
                column: "SubskrybjcaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Platnosci_Subskrybcje_SubskrybjcaID",
                table: "Platnosci",
                column: "SubskrybjcaID",
                principalTable: "Subskrybcje",
                principalColumn: "SubskrybcjaID");
        }
    }
}
