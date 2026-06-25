using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Infrastructure.Dados.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarClube : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clubes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CaminhoEscudo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnoFundacao = table.Column<int>(type: "int", nullable: false),
                    CidadeEstado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LigaCompeticao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstadioPrincipal = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BreveHistoria = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clubes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$Kh7AV.RKXjT1sMD25nHxZu6flyTyKTAiSUpkEUpN66iMe7aSH12a.");

            migrationBuilder.CreateIndex(
                name: "IX_clubes_Nome",
                table: "clubes",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clubes");

            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "SenhaHash",
                value: "$2a$11$/KbemEsWUISFITe1PcLH0uTCa.Xp.c5qbsWQYiDfrlVuf3nwx0aQi");
        }
    }
}
