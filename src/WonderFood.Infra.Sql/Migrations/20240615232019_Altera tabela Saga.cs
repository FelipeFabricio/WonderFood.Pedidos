using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderFood.Infra.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AlteratabelaSaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotivoCancelamento",
                table: "CriarPedidoSagaState",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivoCancelamento",
                table: "CriarPedidoSagaState");
        }
    }
}
