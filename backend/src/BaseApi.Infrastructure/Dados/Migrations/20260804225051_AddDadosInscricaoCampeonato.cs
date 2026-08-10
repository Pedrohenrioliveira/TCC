using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AddDadosInscricaoCampeonato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaminhoComprovantePagamento",
                table: "InscricoesCampeonatos",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CaminhoDocumentoIdentidade",
                table: "InscricoesCampeonatos",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NomeResponsavel",
                table: "InscricoesCampeonatos",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TelefoneResponsavel",
                table: "InscricoesCampeonatos",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 22, 50, 51, 106, DateTimeKind.Utc).AddTicks(3549), new DateTime(2026, 8, 4, 22, 50, 51, 106, DateTimeKind.Utc).AddTicks(3543) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 22, 50, 51, 106, DateTimeKind.Utc).AddTicks(3567), new DateTime(2026, 8, 4, 22, 50, 51, 106, DateTimeKind.Utc).AddTicks(3567) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 22, 50, 51, 106, DateTimeKind.Utc).AddTicks(3572), new DateTime(2026, 8, 4, 22, 50, 51, 106, DateTimeKind.Utc).AddTicks(3572) });

            migrationBuilder.UpdateData(
                table: "clubes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 22, 50, 51, 106, DateTimeKind.Utc).AddTicks(3653), new DateTime(2026, 8, 4, 22, 50, 51, 106, DateTimeKind.Utc).AddTicks(3653) });

            migrationBuilder.UpdateData(
                table: "jogadores",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 22, 50, 51, 106, DateTimeKind.Utc).AddTicks(3618), new DateTime(2026, 8, 4, 22, 50, 51, 106, DateTimeKind.Utc).AddTicks(3618) });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$sf6LB3FcSOvXRDUps84lH.bpw2ALkHZGmRD6vVojACbaK/ueJW.WS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaminhoComprovantePagamento",
                table: "InscricoesCampeonatos");

            migrationBuilder.DropColumn(
                name: "CaminhoDocumentoIdentidade",
                table: "InscricoesCampeonatos");

            migrationBuilder.DropColumn(
                name: "NomeResponsavel",
                table: "InscricoesCampeonatos");

            migrationBuilder.DropColumn(
                name: "TelefoneResponsavel",
                table: "InscricoesCampeonatos");

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 22, 33, 2, 17, DateTimeKind.Utc).AddTicks(3417), new DateTime(2026, 8, 4, 22, 33, 2, 17, DateTimeKind.Utc).AddTicks(3412) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 22, 33, 2, 17, DateTimeKind.Utc).AddTicks(3429), new DateTime(2026, 8, 4, 22, 33, 2, 17, DateTimeKind.Utc).AddTicks(3428) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 22, 33, 2, 17, DateTimeKind.Utc).AddTicks(3432), new DateTime(2026, 8, 4, 22, 33, 2, 17, DateTimeKind.Utc).AddTicks(3432) });

            migrationBuilder.UpdateData(
                table: "clubes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 22, 33, 2, 17, DateTimeKind.Utc).AddTicks(3502), new DateTime(2026, 8, 4, 22, 33, 2, 17, DateTimeKind.Utc).AddTicks(3502) });

            migrationBuilder.UpdateData(
                table: "jogadores",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 4, 22, 33, 2, 17, DateTimeKind.Utc).AddTicks(3472), new DateTime(2026, 8, 4, 22, 33, 2, 17, DateTimeKind.Utc).AddTicks(3472) });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$ouUs2PFfrXpBBkjULO64VO.YBl6PbUS9bpAb4KSASv.t6IyZJdUBu");
        }
    }
}
