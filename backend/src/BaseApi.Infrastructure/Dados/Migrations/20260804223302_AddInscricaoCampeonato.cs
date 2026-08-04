using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AddInscricaoCampeonato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RodadaId1",
                table: "Partidas",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "InscricoesCampeonatos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CampeonatoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClubeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AceitouRegulamento = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataResposta = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscricoesCampeonatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InscricoesCampeonatos_Campeonatos_CampeonatoId",
                        column: x => x.CampeonatoId,
                        principalTable: "Campeonatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InscricoesCampeonatos_clubes_ClubeId",
                        column: x => x.ClubeId,
                        principalTable: "clubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_RodadaId1",
                table: "Partidas",
                column: "RodadaId1");

            migrationBuilder.CreateIndex(
                name: "IX_InscricoesCampeonatos_CampeonatoId",
                table: "InscricoesCampeonatos",
                column: "CampeonatoId");

            migrationBuilder.CreateIndex(
                name: "IX_InscricoesCampeonatos_ClubeId",
                table: "InscricoesCampeonatos",
                column: "ClubeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Rodadas_RodadaId1",
                table: "Partidas",
                column: "RodadaId1",
                principalTable: "Rodadas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Rodadas_RodadaId1",
                table: "Partidas");

            migrationBuilder.DropTable(
                name: "InscricoesCampeonatos");

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
                values: new object[] { new DateTime(2026, 7, 31, 0, 41, 16, 656, DateTimeKind.Utc).AddTicks(2586), new DateTime(2026, 7, 31, 0, 41, 16, 656, DateTimeKind.Utc).AddTicks(2582) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 31, 0, 41, 16, 656, DateTimeKind.Utc).AddTicks(2611), new DateTime(2026, 7, 31, 0, 41, 16, 656, DateTimeKind.Utc).AddTicks(2611) });

            migrationBuilder.UpdateData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 31, 0, 41, 16, 656, DateTimeKind.Utc).AddTicks(2614), new DateTime(2026, 7, 31, 0, 41, 16, 656, DateTimeKind.Utc).AddTicks(2614) });

            migrationBuilder.UpdateData(
                table: "clubes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 31, 0, 41, 16, 656, DateTimeKind.Utc).AddTicks(2689), new DateTime(2026, 7, 31, 0, 41, 16, 656, DateTimeKind.Utc).AddTicks(2689) });

            migrationBuilder.UpdateData(
                table: "jogadores",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 7, 31, 0, 41, 16, 656, DateTimeKind.Utc).AddTicks(2646), new DateTime(2026, 7, 31, 0, 41, 16, 656, DateTimeKind.Utc).AddTicks(2646) });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$xd5uxTXi5FOawfFZsP2yuuCAV9N6rp7ZXAFhwKHVBxWjgZCDXz45m");
        }
    }
}
