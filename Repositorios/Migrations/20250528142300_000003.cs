using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorios.Migrations
{
    /// <inheritdoc />
    public partial class _000003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIOS_PERFILS_PerfilId",
                table: "USUARIOS");

            migrationBuilder.AlterColumn<Guid>(
                name: "PerfilId",
                table: "USUARIOS",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "Perfil",
                table: "USUARIOS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIOS_PERFILS_PerfilId",
                table: "USUARIOS",
                column: "PerfilId",
                principalTable: "PERFILS",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIOS_PERFILS_PerfilId",
                table: "USUARIOS");

            migrationBuilder.DropColumn(
                name: "Perfil",
                table: "USUARIOS");

            migrationBuilder.AlterColumn<Guid>(
                name: "PerfilId",
                table: "USUARIOS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIOS_PERFILS_PerfilId",
                table: "USUARIOS",
                column: "PerfilId",
                principalTable: "PERFILS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
