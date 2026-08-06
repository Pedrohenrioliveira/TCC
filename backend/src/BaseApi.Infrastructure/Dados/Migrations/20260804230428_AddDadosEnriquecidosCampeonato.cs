using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AddDadosEnriquecidosCampeonato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Campeonatos",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Local",
                table: "Campeonatos",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CaminhoLogo",
                table: "Campeonatos",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CaminhoImagemCampo",
                table: "Campeonatos",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ChavePix",
                table: "Campeonatos",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Campeonatos",
                type: "text",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DiasDosJogos",
                table: "Campeonatos",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "TaxaInscricao",
                table: "Campeonatos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CaminhoImagemCampo", "ChavePix", "CriadoEm", "Descricao", "DiasDosJogos", "TaxaInscricao" },
                values: new object[] { new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9297), "", "", new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9293), "", "", 0m });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AtualizadoEm", "CaminhoImagemCampo", "ChavePix", "CriadoEm", "Descricao", "DiasDosJogos", "TaxaInscricao" },
                values: new object[] { new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9313), "", "", new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9313), "", "", 0m });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AtualizadoEm", "CaminhoImagemCampo", "ChavePix", "CriadoEm", "Descricao", "DiasDosJogos", "TaxaInscricao" },
                values: new object[] { new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9316), "", "", new DateTime(2026, 8, 4, 23, 4, 28, 101, DateTimeKind.Utc).AddTicks(9316), "", "", 0m });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaminhoImagemCampo",
                table: "Campeonatos");

            migrationBuilder.DropColumn(
                name: "ChavePix",
                table: "Campeonatos");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Campeonatos");

            migrationBuilder.DropColumn(
                name: "DiasDosJogos",
                table: "Campeonatos");

            migrationBuilder.DropColumn(
                name: "TaxaInscricao",
                table: "Campeonatos");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Campeonatos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Local",
                table: "Campeonatos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CaminhoLogo",
                table: "Campeonatos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
    }
}
