using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Campeonatos.Queries.ListarCampeonatos;

public class ListarCampeonatosQueryHandler(IAppDbContext dbContext) 
    : IRequestHandler<ListarCampeonatosQuery, RespostaApi<IEnumerable<CampeonatoDto>>>
{
    public async Task<RespostaApi<IEnumerable<CampeonatoDto>>> Handle(ListarCampeonatosQuery request, CancellationToken ct)
    {
        var query = dbContext.Campeonatos.AsNoTracking();

        if (!string.IsNullOrEmpty(request.Status) && Enum.TryParse<StatusCampeonato>(request.Status, true, out var statusEnum))
        {
            query = query.Where(c => c.Status == statusEnum);
        }

        var campeonatos = await query
            .Select(c => new CampeonatoDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Local = c.Local,
                DataInicio = c.DataInicio,
                DataFim = c.DataFim,
                Status = c.Status,
                CaminhoLogo = c.CaminhoLogo,
                LimiteEquipes = c.LimiteEquipes,
                TaxaInscricao = c.TaxaInscricao,
                ChavePix = c.ChavePix,
                DiasDosJogos = c.DiasDosJogos,
                Descricao = c.Descricao,
                CaminhoImagemCampo = c.CaminhoImagemCampo,
                VagasDisponiveis = c.LimiteEquipes - dbContext.InscricoesCampeonatos
                    .Count(i => i.CampeonatoId == c.Id && i.Status == StatusInscricao.Aprovada)
            })
            .ToListAsync(ct);

        return RespostaApi<IEnumerable<CampeonatoDto>>.Sucesso(campeonatos);
    }
}
