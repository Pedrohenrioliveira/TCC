using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Campeonatos.Queries.ObterInscricoesCampeonato;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Campeonatos.Queries.ObterMinhasInscricoes;

public class ObterMinhasInscricoesQuery : IRequest<RespostaApi<IEnumerable<InscricaoCampeonatoDto>>>
{
    public Guid ClubeId { get; set; }
}

public class ObterMinhasInscricoesQueryHandler(IAppDbContext dbContext) : IRequestHandler<ObterMinhasInscricoesQuery, RespostaApi<IEnumerable<InscricaoCampeonatoDto>>>
{
    public async Task<RespostaApi<IEnumerable<InscricaoCampeonatoDto>>> Handle(ObterMinhasInscricoesQuery request, CancellationToken cancellationToken)
    {
        var inscricoes = await dbContext.InscricoesCampeonatos
            .Include(i => i.Clube)
            .Where(i => i.ClubeId == request.ClubeId)
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
