using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Campeonatos.Queries.ObterClassificacao;

public class ClassificacaoDto
{
    public Guid ClubeId { get; set; }
    public string NomeClube { get; set; }
    public int Pontos { get; set; }
    public int PartidasJogadas { get; set; }
    public int Vitorias { get; set; }
    public int Empates { get; set; }
    public int Derrotas { get; set; }
    public int GolsPro { get; set; }
    public int GolsContra { get; set; }
    public int SaldoGols => GolsPro - GolsContra;
}

public class ObterClassificacaoQuery : IRequest<RespostaApi<IEnumerable<ClassificacaoDto>>>
{
    public Guid CampeonatoId { get; set; }
}

public class ObterClassificacaoQueryHandler(IAppDbContext dbContext) : IRequestHandler<ObterClassificacaoQuery, RespostaApi<IEnumerable<ClassificacaoDto>>>
{
    public async Task<RespostaApi<IEnumerable<ClassificacaoDto>>> Handle(ObterClassificacaoQuery request, CancellationToken cancellationToken)
    {
        var classif = await dbContext.Classificacoes
            .Include(c => c.Clube)
            .Where(c => c.CampeonatoId == request.CampeonatoId)
            .ToListAsync(cancellationToken);

        var dto = classif.Select(c => new ClassificacaoDto
        {
            ClubeId = c.ClubeId,
            NomeClube = c.Clube.Nome,
            Pontos = c.Pontos,
            PartidasJogadas = c.PartidasJogadas,
            Vitorias = c.Vitorias,
            Empates = c.Empates,
            Derrotas = c.Derrotas,
            GolsPro = c.GolsPro,
            GolsContra = c.GolsContra
        })
        .OrderByDescending(c => c.Pontos)
        .ThenByDescending(c => c.SaldoGols)
        .ThenByDescending(c => c.GolsPro)
        .ToList();

        return RespostaApi<IEnumerable<ClassificacaoDto>>.Sucesso(dto);
    }
}
