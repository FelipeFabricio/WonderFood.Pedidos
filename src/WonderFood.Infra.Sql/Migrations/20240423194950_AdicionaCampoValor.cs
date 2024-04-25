using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderFood.Infra.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaCampoValor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorProduto",
                table: "ProdutosPedido",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorProduto",
                table: "ProdutosPedido");
        }
    }
}
