using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorios.Migrations
{
    /// <inheritdoc />
    public partial class _000004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RG",
                table: "USUARIOS",
                newName: "Rg");

            migrationBuilder.RenameColumn(
                name: "NomeCompleto",
                table: "USUARIOS",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "ENDERECOS",
                newName: "Uf");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "USUARIOS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "ENDERECOS",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "PEDIDOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ITENS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PedidosId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITENS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITENS_PEDIDOS_PedidosId",
                        column: x => x.PedidosId,
                        principalTable: "PEDIDOS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ITENS_PedidosId",
                table: "ITENS",
                column: "PedidosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITENS");

            migrationBuilder.DropTable(
                name: "PEDIDOS");

            migrationBuilder.RenameColumn(
                name: "Rg",
                table: "USUARIOS",
                newName: "RG");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "USUARIOS",
                newName: "NomeCompleto");

            migrationBuilder.RenameColumn(
                name: "Uf",
                table: "ENDERECOS",
                newName: "Estado");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "USUARIOS",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "ENDERECOS",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
