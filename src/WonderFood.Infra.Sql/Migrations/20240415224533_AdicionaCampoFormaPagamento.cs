using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderFood.Infra.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaCampoFormaPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "FormaPagamento",
                table: "Pedidos",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormaPagamento",
                table: "Pedidos");
        }
    }
}
