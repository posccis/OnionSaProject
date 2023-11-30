using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnionSa.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoChavePrimariaCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPFCNPJId",
                table: "Clientes",
                newName: "CPFCNPJ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPFCNPJ",
                table: "Clientes",
                newName: "CPFCNPJId");
        }
    }
}
