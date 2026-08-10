using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Campeonatos.Queries.ObterInscricoesCampeonato;

public class InscricaoCampeonatoDto
{
    public Guid Id { get; set; }
    public Guid CampeonatoId { get; set; }
    public Guid ClubeId { get; set; }
    public string NomeClube { get; set; } = string.Empty;
    public string CaminhoEscudo { get; set; } = string.Empty;
    public string NomeResponsavel { get; set; } = string.Empty;
    public string TelefoneResponsavel { get; set; } = string.Empty;
    public string CaminhoDocumentoIdentidade { get; set; } = string.Empty;
    public string CaminhoComprovantePagamento { get; set; } = string.Empty;
    public StatusInscricao Status { get; set; }
    public bool AceitouRegulamento { get; set; }
    public DateTime DataSolicitacao { get; set; }
}

public class ObterInscricoesCampeonatoQuery : IRequest<RespostaApi<IEnumerable<InscricaoCampeonatoDto>>>
{
    public Guid CampeonatoId { get; set; }
}

public class ObterInscricoesCampeonatoQueryHandler(IAppDbContext dbContext) : IRequestHandler<ObterInscricoesCampeonatoQuery, RespostaApi<IEnumerable<InscricaoCampeonatoDto>>>
{
    public async Task<RespostaApi<IEnumerable<InscricaoCampeonatoDto>>> Handle(ObterInscricoesCampeonatoQuery request, CancellationToken cancellationToken)
    {
        var inscricoes = await dbContext.InscricoesCampeonatos
            .Include(i => i.Clube)
            .Where(i => i.CampeonatoId == request.CampeonatoId)
            .OrderBy(i => i.DataSolicitacao)
            .Select(i => new InscricaoCampeonatoDto
            {
                Id = i.Id,
                CampeonatoId = i.CampeonatoId,
                ClubeId = i.ClubeId,
                NomeClube = i.Clube!.Nome,
                CaminhoEscudo = i.Clube.CaminhoEscudo,
                NomeResponsavel = i.NomeResponsavel,
                TelefoneResponsavel = i.TelefoneResponsavel,
                CaminhoDocumentoIdentidade = i.CaminhoDocumentoIdentidade,
                CaminhoComprovantePagamento = i.CaminhoComprovantePagamento,
                Status = i.Status,
                AceitouRegulamento = i.AceitouRegulamento,
                DataSolicitacao = i.DataSolicitacao
            })
            .ToListAsync(cancellationToken);

        return RespostaApi<IEnumerable<InscricaoCampeonatoDto>>.Sucesso(inscricoes);
    }
}
