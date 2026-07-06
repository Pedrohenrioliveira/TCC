using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoJogadorEClubes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campeonatos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Local = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CaminhoLogo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LimiteEquipes = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campeonatos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SolicitacoesClubes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    JogadorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClubeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Mensagem = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataResposta = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacoesClubes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacoesClubes_clubes_ClubeId",
                        column: x => x.ClubeId,
                        principalTable: "clubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacoesClubes_jogadores_JogadorId",
                        column: x => x.JogadorId,
                        principalTable: "jogadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Campeonatos",
                columns: new[] { "Id", "AtualizadoEm", "CaminhoLogo", "CriadoEm", "DataFim", "DataInicio", "LimiteEquipes", "Local", "Nome", "Status" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3794), "assets/campeonato1.jpg", new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3788), new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 16, "Estádio Municipal", "Copa Regional Norte 2026", 1 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3809), "assets/campeonato2.jpg", new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3809), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 32, "Vários Estádios", "Liga dos Campeões Amadora", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3814), "assets/campeonato3.jpg", new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3814), new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, "Arena Sul", "Torneio de Inverno 2025", 3 }
                });

            migrationBuilder.InsertData(
                table: "clubes",
                columns: new[] { "Id", "AnoFundacao", "AtualizadoEm", "BreveHistoria", "CaminhoEscudo", "CidadeEstado", "CriadoEm", "EstadioPrincipal", "LigaCompeticao", "Nome", "UsuarioId" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), 1990, new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3878), "Um clube criado para testes.", "https://robohash.org/clube1?set=set1", "São Paulo / SP", new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3878), null, "Série A", "Clube Atlético Teste", new Guid("00000000-0000-0000-0000-000000000001") });

            migrationBuilder.InsertData(
                table: "jogadores",
                columns: new[] { "Id", "Altura", "AtualizadoEm", "BioHistorico", "CaminhoFoto", "ClubeId", "CriadoEm", "DataNascimento", "NomeCompleto", "PePreferencial", "Peso", "PosicaoPrincipal", "PosicaoSecundaria", "UsuarioId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), 180, new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3855), "Jogador de teste do sistema.", "https://robohash.org/pedro?set=set5", null, new DateTime(2026, 7, 6, 22, 42, 49, 317, DateTimeKind.Utc).AddTicks(3855), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pedro Oliveira (Teste)", 3, 75.0, 6, null, new Guid("00000000-0000-0000-0000-000000000001") });

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$Wy4ThKYuokPaGofE2Io1rOSBdABkfteADlk97aOBbGycT51l96YsC");

            migrationBuilder.InsertData(
                table: "SolicitacoesClubes",
                columns: new[] { "Id", "ClubeId", "DataResposta", "DataSolicitacao", "JogadorId", "Mensagem", "Status" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000001"), "Gostaria de participar da seletiva do Clube Atlético Teste.", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacoesClubes_ClubeId",
                table: "SolicitacoesClubes",
                column: "ClubeId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacoesClubes_JogadorId",
                table: "SolicitacoesClubes",
                column: "JogadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campeonatos");

            migrationBuilder.DropTable(
                name: "SolicitacoesClubes");

            migrationBuilder.DeleteData(
                table: "clubes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "jogadores",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$fhRhVoVxcHXCkN4DOugViOF1a8UwVg/xvQfGzmzz64Gx5GsSt1zFK");
        }
    }
}
