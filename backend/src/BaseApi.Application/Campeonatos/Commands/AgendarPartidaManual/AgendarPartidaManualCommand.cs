using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Campeonatos.Commands.AgendarPartidaManual;

public class AgendarPartidaManualCommand : IRequest<RespostaApi<Guid>>
{
    public Guid CampeonatoId { get; set; }
    public int NumeroRodada { get; set; }
    public Guid ClubeMandanteId { get; set; }
    public Guid ClubeVisitanteId { get; set; }
    public DateTime DataHora { get; set; }
    public string Local { get; set; } = string.Empty;
}

public class AgendarPartidaManualCommandHandler(IAppDbContext dbContext) : IRequestHandler<AgendarPartidaManualCommand, RespostaApi<Guid>>
{
    public async Task<RespostaApi<Guid>> Handle(AgendarPartidaManualCommand request, CancellationToken cancellationToken)
    {
        if (request.ClubeMandanteId == request.ClubeVisitanteId)
            return RespostaApi<Guid>.Falha("Um clube não pode jogar contra si mesmo.");

        var campeonato = await dbContext.Campeonatos
            .FirstOrDefaultAsync(c => c.Id == request.CampeonatoId, cancellationToken);

        if (campeonato == null)
            return RespostaApi<Guid>.Falha("Campeonato não encontrado.");

        var mandanteInscrito = await dbContext.InscricoesCampeonatos
            .AnyAsync(i => i.CampeonatoId == request.CampeonatoId && i.ClubeId == request.ClubeMandanteId && i.Status == StatusInscricao.Aprovada, cancellationToken);
            
        var visitanteInscrito = await dbContext.InscricoesCampeonatos
            .AnyAsync(i => i.CampeonatoId == request.CampeonatoId && i.ClubeId == request.ClubeVisitanteId && i.Status == StatusInscricao.Aprovada, cancellationToken);

        if (!mandanteInscrito || !visitanteInscrito)
            return RespostaApi<Guid>.Falha("Ambos os clubes devem estar aprovados no campeonato.");

        // Find or create Rodada
        var rodada = await dbContext.Rodadas
            .FirstOrDefaultAsync(r => r.CampeonatoId == request.CampeonatoId && r.Numero == request.NumeroRodada, cancellationToken);

        if (rodada == null)
            {
                rodada = new Rodada
                {
                    Id = Guid.NewGuid(),
                    CampeonatoId = request.CampeonatoId,
                    Numero = request.NumeroRodada
                };
                dbContext.Rodadas.Add(rodada);
            }

        var partida = new Partida
        {
            Id = Guid.NewGuid(),
            RodadaId = rodada.Id,
            ClubeMandanteId = request.ClubeMandanteId,
            ClubeVisitanteId = request.ClubeVisitanteId,
            DataHora = request.DataHora.ToUniversalTime(),
            Local = request.Local,
            Status = StatusPartida.Agendada
        };

        dbContext.Partidas.Add(partida);

        if (campeonato.Status == StatusCampeonato.Aberto)
        {
            campeonato.Status = StatusCampeonato.EmAndamento;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<Guid>.Sucesso(partida.Id, "Partida agendada com sucesso.");
    }
}
