using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorios.Migrations
{
    /// <inheritdoc />
    public partial class _000007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedidos_ITENS_ItensId",
                table: "ItensPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedidos_PEDIDOS_PedidosId",
                table: "ItensPedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItensPedidos",
                table: "ItensPedidos");

            migrationBuilder.RenameTable(
                name: "ItensPedidos",
                newName: "PedidoItens");

            migrationBuilder.RenameIndex(
                name: "IX_ItensPedidos_PedidosId",
                table: "PedidoItens",
                newName: "IX_PedidoItens_PedidosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoItens",
                table: "PedidoItens",
                columns: new[] { "ItensId", "PedidosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_ITENS_ItensId",
                table: "PedidoItens",
                column: "ItensId",
                principalTable: "ITENS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_PEDIDOS_PedidosId",
                table: "PedidoItens",
                column: "PedidosId",
                principalTable: "PEDIDOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_ITENS_ItensId",
                table: "PedidoItens");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_PEDIDOS_PedidosId",
                table: "PedidoItens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoItens",
                table: "PedidoItens");

            migrationBuilder.RenameTable(
                name: "PedidoItens",
                newName: "ItensPedidos");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoItens_PedidosId",
                table: "ItensPedidos",
                newName: "IX_ItensPedidos_PedidosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItensPedidos",
                table: "ItensPedidos",
                columns: new[] { "ItensId", "PedidosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedidos_ITENS_ItensId",
                table: "ItensPedidos",
                column: "ItensId",
                principalTable: "ITENS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedidos_PEDIDOS_PedidosId",
                table: "ItensPedidos",
                column: "PedidosId",
                principalTable: "PEDIDOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
