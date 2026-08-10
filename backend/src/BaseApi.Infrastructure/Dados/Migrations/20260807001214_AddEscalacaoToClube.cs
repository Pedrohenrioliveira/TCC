using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AddEscalacaoToClube : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EscalacaoJson",
                table: "clubes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9403), new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9398) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9427), new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9427) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9430), new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "clubes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm", "EscalacaoJson" },
                values: new object[] { new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9520), new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9519), null });

            migrationBuilder.UpdateData(
                table: "jogadores",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9471), new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9471) });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$G9Zh7bfSLCehYlsdNq1/1u.BQx/LJCcb8pAmfzlsm8M1JRiWOhCW6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EscalacaoJson",
                table: "clubes");

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
    }
}
