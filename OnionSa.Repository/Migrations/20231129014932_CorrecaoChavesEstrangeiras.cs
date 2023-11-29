using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnionSa.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoChavesEstrangeiras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Pedidos",
                newName: "CPFCNPJ");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                newName: "IX_Pedidos_CPFCNPJ");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_CPFCNPJ",
                table: "Pedidos",
                column: "CPFCNPJ",
                principalTable: "Clientes",
                principalColumn: "CPFCNPJ",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_CPFCNPJ",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "CPFCNPJ",
                table: "Pedidos",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_CPFCNPJ",
                table: "Pedidos",
                newName: "IX_Pedidos_ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "CPFCNPJ",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
