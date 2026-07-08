using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageFieldsToLongText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CaminhoFoto",
                table: "jogadores",
                type: "LONGTEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CaminhoEscudo",
                table: "clubes",
                type: "LONGTEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 8, 0, 15, 7, 415, DateTimeKind.Utc).AddTicks(5277), new DateTime(2026, 7, 8, 0, 15, 7, 415, DateTimeKind.Utc).AddTicks(5272) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 8, 0, 15, 7, 415, DateTimeKind.Utc).AddTicks(5294), new DateTime(2026, 7, 8, 0, 15, 7, 415, DateTimeKind.Utc).AddTicks(5293) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 8, 0, 15, 7, 415, DateTimeKind.Utc).AddTicks(5298), new DateTime(2026, 7, 8, 0, 15, 7, 415, DateTimeKind.Utc).AddTicks(5298) });

            migrationBuilder.UpdateData(
                table: "clubes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 8, 0, 15, 7, 415, DateTimeKind.Utc).AddTicks(5369), new DateTime(2026, 7, 8, 0, 15, 7, 415, DateTimeKind.Utc).AddTicks(5369) });

            migrationBuilder.UpdateData(
                table: "jogadores",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 8, 0, 15, 7, 415, DateTimeKind.Utc).AddTicks(5340), new DateTime(2026, 7, 8, 0, 15, 7, 415, DateTimeKind.Utc).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$KHXe9p6ESRSVB9O1diA55OJxeXZleea6sykaGMXk/1MAsNl5bs2eq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CaminhoFoto",
                table: "jogadores",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "LONGTEXT")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CaminhoEscudo",
                table: "clubes",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "LONGTEXT")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3794), new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3788) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3809), new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3809) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3814), new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3814) });

            migrationBuilder.UpdateData(
                table: "clubes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3878), new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3878) });

            migrationBuilder.UpdateData(
                table: "jogadores",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3855), new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3855) });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$Wy4ThKYuokPaGofE2Io1rOSBdABkfteADlk97aOBbGycT51l96YsC");
        }
    }
}
