using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderFood.Infra.Sql.Migrations
{
    /// <inheritdoc />
    public partial class Alteracammpomotivocancelamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MotivoCancelamento",
                table: "CriarPedidoSagaState",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CriarPedidoSagaState",
                keyColumn: "MotivoCancelamento",
                keyValue: null,
                column: "MotivoCancelamento",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MotivoCancelamento",
                table: "CriarPedidoSagaState",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
