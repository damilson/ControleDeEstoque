using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorios.Migrations
{
    /// <inheritdoc />
    public partial class _000009 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosITens_ITENS_ItemId",
                table: "PedidosITens");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidosITens_PEDIDOS_PedidoId",
                table: "PedidosITens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidosITens",
                table: "PedidosITens");

            migrationBuilder.RenameTable(
                name: "PedidosITens",
                newName: "PedidosItens");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosITens_PedidoId",
                table: "PedidosItens",
                newName: "IX_PedidosItens_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosITens_ItemId",
                table: "PedidosItens",
                newName: "IX_PedidosItens_ItemId");

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "PedidosItens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidosItens",
                table: "PedidosItens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosItens_ITENS_ItemId",
                table: "PedidosItens",
                column: "ItemId",
                principalTable: "ITENS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosItens_PEDIDOS_PedidoId",
                table: "PedidosItens",
                column: "PedidoId",
                principalTable: "PEDIDOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosItens_ITENS_ItemId",
                table: "PedidosItens");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidosItens_PEDIDOS_PedidoId",
                table: "PedidosItens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidosItens",
                table: "PedidosItens");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "PedidosItens");

            migrationBuilder.RenameTable(
                name: "PedidosItens",
                newName: "PedidosITens");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosItens_PedidoId",
                table: "PedidosITens",
                newName: "IX_PedidosITens_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosItens_ItemId",
                table: "PedidosITens",
                newName: "IX_PedidosITens_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidosITens",
                table: "PedidosITens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosITens_ITENS_ItemId",
                table: "PedidosITens",
                column: "ItemId",
                principalTable: "ITENS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosITens_PEDIDOS_PedidoId",
                table: "PedidosITens",
                column: "PedidoId",
                principalTable: "PEDIDOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
