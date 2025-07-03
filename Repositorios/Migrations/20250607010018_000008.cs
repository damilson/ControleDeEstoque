using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorios.Migrations
{
    /// <inheritdoc />
    public partial class _000008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoItens");

            migrationBuilder.CreateTable(
                name: "PedidosITens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosITens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosITens_ITENS_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ITENS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosITens_PEDIDOS_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "PEDIDOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidosITens_ItemId",
                table: "PedidosITens",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosITens_PedidoId",
                table: "PedidosITens",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosITens");

            migrationBuilder.CreateTable(
                name: "PedidoItens",
                columns: table => new
                {
                    ItensId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItens", x => new { x.ItensId, x.PedidosId });
                    table.ForeignKey(
                        name: "FK_PedidoItens_ITENS_ItensId",
                        column: x => x.ItensId,
                        principalTable: "ITENS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoItens_PEDIDOS_PedidosId",
                        column: x => x.PedidosId,
                        principalTable: "PEDIDOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_PedidosId",
                table: "PedidoItens",
                column: "PedidosId");
        }
    }
}
