using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorios.Migrations
{
    /// <inheritdoc />
    public partial class _000006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ITENS_PEDIDOS_PedidosId",
                table: "ITENS");

            migrationBuilder.DropIndex(
                name: "IX_ITENS_PedidosId",
                table: "ITENS");

            migrationBuilder.DropColumn(
                name: "PedidosId",
                table: "ITENS");

            migrationBuilder.CreateTable(
                name: "ItensPedidos",
                columns: table => new
                {
                    ItensId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedidos", x => new { x.ItensId, x.PedidosId });
                    table.ForeignKey(
                        name: "FK_ItensPedidos_ITENS_ItensId",
                        column: x => x.ItensId,
                        principalTable: "ITENS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensPedidos_PEDIDOS_PedidosId",
                        column: x => x.PedidosId,
                        principalTable: "PEDIDOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedidos_PedidosId",
                table: "ItensPedidos",
                column: "PedidosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensPedidos");

            migrationBuilder.AddColumn<Guid>(
                name: "PedidosId",
                table: "ITENS",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ITENS_PedidosId",
                table: "ITENS",
                column: "PedidosId");

            migrationBuilder.AddForeignKey(
                name: "FK_ITENS_PEDIDOS_PedidosId",
                table: "ITENS",
                column: "PedidosId",
                principalTable: "PEDIDOS",
                principalColumn: "Id");
        }
    }
}
