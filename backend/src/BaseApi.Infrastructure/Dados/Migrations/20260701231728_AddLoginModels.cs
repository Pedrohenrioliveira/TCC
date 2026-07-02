using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeUsuario",
                table: "usuarios",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "jogadores",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "clubes",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.InsertData(
                table: "perfis",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 4, "Perfil de acesso para Jogadores", "Jogador" },
                    { 5, "Perfil de acesso para Clubes", "Clube" }
                });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "NomeUsuario", "SenhaHash" },
                values: new object[] { "admin", "$2a$11$fhRhVoVxcHXCkN4DOugViOF1a8UwVg/xvQfGzmzz64Gx5GsSt1zFK" });

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_NomeUsuario",
                table: "usuarios",
                column: "NomeUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_jogadores_UsuarioId",
                table: "jogadores",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_clubes_UsuarioId",
                table: "clubes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_clubes_usuarios_UsuarioId",
                table: "clubes",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jogadores_usuarios_UsuarioId",
                table: "jogadores",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clubes_usuarios_UsuarioId",
                table: "clubes");

            migrationBuilder.DropForeignKey(
                name: "FK_jogadores_usuarios_UsuarioId",
                table: "jogadores");

            migrationBuilder.DropIndex(
                name: "IX_usuarios_NomeUsuario",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_jogadores_UsuarioId",
                table: "jogadores");

            migrationBuilder.DropIndex(
                name: "IX_clubes_UsuarioId",
                table: "clubes");

            migrationBuilder.DeleteData(
                table: "perfis",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "perfis",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "NomeUsuario",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "jogadores");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "clubes");

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$nR4.b9vAFrZBe1ZUvf1MSuHvf5Yf/8Bl3C1RohC55zyrYnxu5YALu");
        }
    }
}
