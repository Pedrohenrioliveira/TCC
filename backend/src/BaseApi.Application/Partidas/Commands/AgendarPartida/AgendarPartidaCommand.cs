using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Partidas.Commands.AgendarPartida;

public class AgendarPartidaCommand : IRequest<RespostaApi<Guid>>
{
    public Guid RodadaId { get; set; }
    public Guid ClubeMandanteId { get; set; }
    public Guid ClubeVisitanteId { get; set; }
    public DateTime DataHora { get; set; }
    public string Local { get; set; } = string.Empty;
}

public class AgendarPartidaCommandHandler(IAppDbContext dbContext) : IRequestHandler<AgendarPartidaCommand, RespostaApi<Guid>>
{
    public async Task<RespostaApi<Guid>> Handle(AgendarPartidaCommand request, CancellationToken cancellationToken)
    {
        if (request.ClubeMandanteId == request.ClubeVisitanteId)
            return RespostaApi<Guid>.Falha("Um clube não pode jogar contra si mesmo.");

        var rodada = await dbContext.Rodadas
            .Include(r => r.Campeonato)
            .FirstOrDefaultAsync(r => r.Id == request.RodadaId, cancellationToken);
            
        if (rodada == null)
            return RespostaApi<Guid>.Falha("Rodada não encontrada.");

        var mandanteInscrito = await dbContext.Classificacoes
            .AnyAsync(c => c.CampeonatoId == rodada.CampeonatoId && c.ClubeId == request.ClubeMandanteId, cancellationToken);
            
        var visitanteInscrito = await dbContext.Classificacoes
            .AnyAsync(c => c.CampeonatoId == rodada.CampeonatoId && c.ClubeId == request.ClubeVisitanteId, cancellationToken);

        if (!mandanteInscrito || !visitanteInscrito)
            return RespostaApi<Guid>.Falha("Ambos os clubes devem estar inscritos no campeonato.");

        var partida = new Partida
        {
            RodadaId = request.RodadaId,
            ClubeMandanteId = request.ClubeMandanteId,
            ClubeVisitanteId = request.ClubeVisitanteId,
            DataHora = request.DataHora,
            Local = request.Local,
            Status = StatusPartida.Agendada
        };

        dbContext.Partidas.Add(partida);
        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<Guid>.Sucesso(partida.Id, "Partida agendada com sucesso.");
    }
}
