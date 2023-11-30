using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnionSa.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoDropChavePrimariaCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_CPFCNPJ",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CPFCNPJ",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "preco",
                table: "Produtos",
                newName: "Preco");

            migrationBuilder.AlterColumn<long>(
                name: "CPFCNPJ",
                table: "Pedidos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "CPFCNPJId",
                table: "Clientes",
                type: "bigint",
                maxLength: 14,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "CPFCNPJId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_CPFCNPJ",
                table: "Pedidos",
                column: "CPFCNPJ",
                principalTable: "Clientes",
                principalColumn: "CPFCNPJId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_CPFCNPJ",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CPFCNPJId",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "Produtos",
                newName: "preco");

            migrationBuilder.AlterColumn<int>(
                name: "CPFCNPJ",
                table: "Pedidos",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "CPFCNPJ",
                table: "Clientes",
                type: "int",
                maxLength: 14,
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "CPFCNPJ");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_CPFCNPJ",
                table: "Pedidos",
                column: "CPFCNPJ",
                principalTable: "Clientes",
                principalColumn: "CPFCNPJ",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
