using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AddPostagemFeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClubeCampeaoId",
                table: "Campeonatos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "Postagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CaminhoFoto = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataPostagem = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    JogadorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ClubeId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Postagens_clubes_ClubeId",
                        column: x => x.ClubeId,
                        principalTable: "clubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Postagens_jogadores_JogadorId",
                        column: x => x.JogadorId,
                        principalTable: "jogadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "ClubeCampeaoId", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 11, 23, 12, 20, 569, DateTimeKind.Utc).AddTicks(1652), null, new DateTime(2026, 8, 11, 23, 12, 20, 569, DateTimeKind.Utc).AddTicks(1647) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AtualizadoEm", "ClubeCampeaoId", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 11, 23, 12, 20, 569, DateTimeKind.Utc).AddTicks(1669), null, new DateTime(2026, 8, 11, 23, 12, 20, 569, DateTimeKind.Utc).AddTicks(1668) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AtualizadoEm", "ClubeCampeaoId", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 11, 23, 12, 20, 569, DateTimeKind.Utc).AddTicks(1672), null, new DateTime(2026, 8, 11, 23, 12, 20, 569, DateTimeKind.Utc).AddTicks(1672) });

            migrationBuilder.UpdateData(
                table: "clubes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 11, 23, 12, 20, 569, DateTimeKind.Utc).AddTicks(1743), new DateTime(2026, 8, 11, 23, 12, 20, 569, DateTimeKind.Utc).AddTicks(1743) });

            migrationBuilder.UpdateData(
                table: "jogadores",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 8, 11, 23, 12, 20, 569, DateTimeKind.Utc).AddTicks(1713), new DateTime(2026, 8, 11, 23, 12, 20, 569, DateTimeKind.Utc).AddTicks(1713) });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$ApOE/jB8WocUtZlV34wVeOzxO3z0eP99w2TW9V9osaOfftetsGuAO");

            migrationBuilder.CreateIndex(
                name: "IX_Campeonatos_ClubeCampeaoId",
                table: "Campeonatos",
                column: "ClubeCampeaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_ClubeId",
                table: "Postagens",
                column: "ClubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_JogadorId",
                table: "Postagens",
                column: "JogadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campeonatos_clubes_ClubeCampeaoId",
                table: "Campeonatos",
                column: "ClubeCampeaoId",
                principalTable: "clubes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campeonatos_clubes_ClubeCampeaoId",
                table: "Campeonatos");

            migrationBuilder.DropTable(
                name: "Postagens");

            migrationBuilder.DropIndex(
                name: "IX_Campeonatos_ClubeCampeaoId",
                table: "Campeonatos");

            migrationBuilder.DropColumn(
                name: "ClubeCampeaoId",
                table: "Campeonatos");

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
    }
}
