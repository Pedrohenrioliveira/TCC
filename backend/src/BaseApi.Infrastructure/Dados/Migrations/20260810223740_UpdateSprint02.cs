using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSprint02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 10, 22, 37, 40, 234, DateTimeKind.Utc).AddTicks(2906), new DateTime(2026, 8, 10, 22, 37, 40, 234, DateTimeKind.Utc).AddTicks(2901) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 10, 22, 37, 40, 234, DateTimeKind.Utc).AddTicks(2923), new DateTime(2026, 8, 10, 22, 37, 40, 234, DateTimeKind.Utc).AddTicks(2923) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 10, 22, 37, 40, 234, DateTimeKind.Utc).AddTicks(2926), new DateTime(2026, 8, 10, 22, 37, 40, 234, DateTimeKind.Utc).AddTicks(2926) });

            migrationBuilder.UpdateData(
                table: "clubes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 10, 22, 37, 40, 234, DateTimeKind.Utc).AddTicks(2992), new DateTime(2026, 8, 10, 22, 37, 40, 234, DateTimeKind.Utc).AddTicks(2992) });

            migrationBuilder.UpdateData(
                table: "jogadores",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 10, 22, 37, 40, 234, DateTimeKind.Utc).AddTicks(2963), new DateTime(2026, 8, 10, 22, 37, 40, 234, DateTimeKind.Utc).AddTicks(2963) });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$F48bb.aiy3HwRRS2BbsTdO76rwoD/1DORkbSM4KX8.zBNnzASBDD6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9520), new DateTime(2026, 8, 7, 0, 12, 13, 881, DateTimeKind.Utc).AddTicks(9519) });

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
    }
}
