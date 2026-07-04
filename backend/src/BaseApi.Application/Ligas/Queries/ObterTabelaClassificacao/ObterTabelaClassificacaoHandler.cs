using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Ligas.Queries.ObterTabelaClassificacao;

public class ObterTabelaClassificacaoHandler : IRequestHandler<ObterTabelaClassificacaoQuery, TabelaClassificacaoDto>
{
    public Task<TabelaClassificacaoDto> Handle(ObterTabelaClassificacaoQuery request, CancellationToken cancellationToken)
    {
        // Mock de dados para a UI
        var times = new List<TimeClassificacaoDto>
        {
            new(Guid.NewGuid(), "Flamengo", "assets/escudos/fla.png", 1, 45, 20, 14, 3, 3, 35, 12, 23),
            new(Guid.NewGuid(), "Palmeiras", "assets/escudos/pal.png", 2, 42, 20, 12, 6, 2, 30, 15, 15),
            new(Guid.NewGuid(), "São Paulo", "assets/escudos/sao.png", 3, 38, 20, 11, 5, 4, 25, 18, 7),
            new(Guid.NewGuid(), "Corinthians", "assets/escudos/cor.png", 4, 35, 20, 10, 5, 5, 22, 20, 2),
            new(Guid.NewGuid(), "Fluminense", "assets/escudos/flu.png", 5, 30, 20, 8, 6, 6, 20, 22, -2)
        };

        var tabela = new TabelaClassificacaoDto(
            LigaId: request.LigaId,
            NomeLiga: "Liga Nacional A",
            Times: times
        );

        return Task.FromResult(tabela);
    }
}
