using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class FixPartidaRodadaMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Rodadas_RodadaId1",
                table: "Partidas");

            migrationBuilder.DropIndex(
                name: "IX_Partidas_RodadaId1",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "RodadaId1",
                table: "Partidas");

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 6, 23, 8, 51, 615, DateTimeKind.Utc).AddTicks(5962), new DateTime(2026, 8, 6, 23, 8, 51, 615, DateTimeKind.Utc).AddTicks(5959) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 6, 23, 8, 51, 615, DateTimeKind.Utc).AddTicks(5977), new DateTime(2026, 8, 6, 23, 8, 51, 615, DateTimeKind.Utc).AddTicks(5977) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 6, 23, 8, 51, 615, DateTimeKind.Utc).AddTicks(5980), new DateTime(2026, 8, 6, 23, 8, 51, 615, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                table: "clubes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 6, 23, 8, 51, 615, DateTimeKind.Utc).AddTicks(6050), new DateTime(2026, 8, 6, 23, 8, 51, 615, DateTimeKind.Utc).AddTicks(6050) });

            migrationBuilder.UpdateData(
                table: "jogadores",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 6, 23, 8, 51, 615, DateTimeKind.Utc).AddTicks(6015), new DateTime(2026, 8, 6, 23, 8, 51, 615, DateTimeKind.Utc).AddTicks(6015) });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$zTVYyQq9u/tRNQwHa6Xpo.YsiLDzaL6vavcA8Ht/eK0buOUcshiiO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RodadaId1",
                table: "Partidas",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9297), new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9293) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9313), new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9313) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9316), new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9316) });

            migrationBuilder.UpdateData(
                table: "clubes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9410), new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9410) });

            migrationBuilder.UpdateData(
                table: "jogadores",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9362), new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9361) });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$va7whIsfJaLgixgIcxI5ZeoXO4MJ7QjRcI4tYfjZu6xA/lLnu9LYy");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_RodadaId1",
                table: "Partidas",
                column: "RodadaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Rodadas_RodadaId1",
                table: "Partidas",
                column: "RodadaId1",
                principalTable: "Rodadas",
                principalColumn: "Id");
        }
    }
}
