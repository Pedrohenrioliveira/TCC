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

namespace BaseApi.Application.Campeonatos.Queries.ObterRodadasCampeonato;

public class PartidaDto
{
    public Guid Id { get; set; }
    public string NomeMandante { get; set; } = string.Empty;
    public string NomeVisitante { get; set; } = string.Empty;
    public int? GolsMandante { get; set; }
    public int? GolsVisitante { get; set; }
    public DateTime DataHora { get; set; }
    public string Local { get; set; } = string.Empty;
    public StatusPartida Status { get; set; }
}

public class RodadaDto
{
    public Guid Id { get; set; }
    public int Numero { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<PartidaDto> Partidas { get; set; } = new();
}

public class ObterRodadasCampeonatoQuery : IRequest<RespostaApi<IEnumerable<RodadaDto>>>
{
    public Guid CampeonatoId { get; set; }
}

public class ObterRodadasCampeonatoQueryHandler(IAppDbContext dbContext) : IRequestHandler<ObterRodadasCampeonatoQuery, RespostaApi<IEnumerable<RodadaDto>>>
{
    public async Task<RespostaApi<IEnumerable<RodadaDto>>> Handle(ObterRodadasCampeonatoQuery request, CancellationToken cancellationToken)
    {
        var rodadas = await dbContext.Rodadas
            .Include(r => r.Partidas)
                .ThenInclude(p => p.ClubeMandante)
            .Include(r => r.Partidas)
                .ThenInclude(p => p.ClubeVisitante)
            .Where(r => r.CampeonatoId == request.CampeonatoId)
            .OrderBy(r => r.Numero)
            .ToListAsync(cancellationToken);

        var dto = rodadas.Select(r => new RodadaDto
        {
            Id = r.Id,
            Numero = r.Numero,
            Nome = r.Nome,
            Partidas = r.Partidas.OrderBy(p => p.DataHora).Select(p => new PartidaDto
            {
                Id = p.Id,
                NomeMandante = p.ClubeMandante.Nome,
                NomeVisitante = p.ClubeVisitante.Nome,
                GolsMandante = p.GolsMandante,
                GolsVisitante = p.GolsVisitante,
                DataHora = p.DataHora,
                Local = p.Local,
                Status = p.Status
            }).ToList()
        }).ToList();

        return RespostaApi<IEnumerable<RodadaDto>>.Sucesso(dto);
    }
}
