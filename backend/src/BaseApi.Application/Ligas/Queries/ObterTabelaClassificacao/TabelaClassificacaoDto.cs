using System;
using System.Collections.Generic;

namespace BaseApi.Application.Ligas.Queries.ObterTabelaClassificacao;

public record TimeClassificacaoDto(
    Guid ClubeId,
    string NomeClube,
    string EscudoUrl,
    int Posicao,
    int Pontos,
    int Jogos,
    int Vitorias,
    int Empates,
    int Derrotas,
    int GolsPro,
    int GolsContra,
    int SaldoGols
);

public record TabelaClassificacaoDto(
    Guid LigaId,
    string NomeLiga,
    List<TimeClassificacaoDto> Times
);
