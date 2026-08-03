using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AddCompeticaoEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classificacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CampeonatoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClubeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Pontos = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    PartidasJogadas = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Vitorias = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Empates = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Derrotas = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    GolsPro = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    GolsContra = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classificacoes_Campeonatos_CampeonatoId",
                        column: x => x.CampeonatoId,
                        principalTable: "Campeonatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classificacoes_clubes_ClubeId",
                        column: x => x.ClubeId,
                        principalTable: "clubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rodadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CampeonatoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rodadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rodadas_Campeonatos_CampeonatoId",
                        column: x => x.CampeonatoId,
                        principalTable: "Campeonatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RodadaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClubeMandanteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClubeVisitanteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GolsMandante = table.Column<int>(type: "int", nullable: true),
                    GolsVisitante = table.Column<int>(type: "int", nullable: true),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Local = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidas_Rodadas_RodadaId",
                        column: x => x.RodadaId,
                        principalTable: "Rodadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partidas_clubes_ClubeMandanteId",
                        column: x => x.ClubeMandanteId,
                        principalTable: "clubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partidas_clubes_ClubeVisitanteId",
                        column: x => x.ClubeVisitanteId,
                        principalTable: "clubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_Classificacoes_CampeonatoId_ClubeId",
                table: "Classificacoes",
                columns: new[] { "CampeonatoId", "ClubeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classificacoes_ClubeId",
                table: "Classificacoes",
                column: "ClubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_ClubeMandanteId",
                table: "Partidas",
                column: "ClubeMandanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_ClubeVisitanteId",
                table: "Partidas",
                column: "ClubeVisitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_RodadaId",
                table: "Partidas",
                column: "RodadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rodadas_CampeonatoId",
                table: "Rodadas",
                column: "CampeonatoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classificacoes");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "Rodadas");

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
    }
}
