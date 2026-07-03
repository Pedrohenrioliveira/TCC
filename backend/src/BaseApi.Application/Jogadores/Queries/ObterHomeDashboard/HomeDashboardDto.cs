using System;

namespace BaseApi.Application.Jogadores.Queries.ObterHomeDashboard;

public record HomeDashboardDto(
    Guid JogadorId,
    string CaminhoFoto,
    string NomeCompleto,
    string PosicaoPrincipal,
    int Nivel,
    int GolsNaTemporada,
    int Assistencias,
    decimal VariacaoGols,
    decimal VariacaoAssistencias
);
