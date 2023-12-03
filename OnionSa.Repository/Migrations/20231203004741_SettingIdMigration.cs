using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnionSa.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SettingIdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Pedidos");

            migrationBuilder.AddColumn<string>(
                name: "Ido",
                table: "Pedidos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Ido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Ido",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Id");
        }
    }
}
