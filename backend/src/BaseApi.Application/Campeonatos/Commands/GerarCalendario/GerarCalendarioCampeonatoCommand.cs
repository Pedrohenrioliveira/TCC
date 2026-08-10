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

namespace BaseApi.Application.Campeonatos.Commands.GerarCalendario;

public class GerarCalendarioCampeonatoCommand : IRequest<RespostaApi<bool>>
{
    public Guid CampeonatoId { get; set; }
}

public class GerarCalendarioCampeonatoCommandHandler(IAppDbContext dbContext) : IRequestHandler<GerarCalendarioCampeonatoCommand, RespostaApi<bool>>
{
    public async Task<RespostaApi<bool>> Handle(GerarCalendarioCampeonatoCommand request, CancellationToken cancellationToken)
    {
        var campeonato = await dbContext.Campeonatos
            .FirstOrDefaultAsync(c => c.Id == request.CampeonatoId, cancellationToken);

        if (campeonato == null)
            return RespostaApi<bool>.Falha("Campeonato não encontrado.");

        if (campeonato.Status != StatusCampeonato.Aberto)
            return RespostaApi<bool>.Falha("O calendário só pode ser gerado para campeonatos abertos.");

        var clubesInscritos = await dbContext.InscricoesCampeonatos
            .Where(i => i.CampeonatoId == request.CampeonatoId && i.Status == StatusInscricao.Aprovada)
            .Select(i => i.ClubeId)
            .ToListAsync(cancellationToken);

        if (clubesInscritos.Count < 2)
            return RespostaApi<bool>.Falha("É necessário aprovar pelo menos 2 clubes para gerar o calendário.");

        // Check if rounds already exist
        var rodadasExistentes = await dbContext.Rodadas
            .AnyAsync(r => r.CampeonatoId == request.CampeonatoId, cancellationToken);
            
        if (rodadasExistentes)
            return RespostaApi<bool>.Falha("O calendário já foi gerado para este campeonato.");

        // Round-Robin Algorithm
        var times = new List<Guid>(clubesInscritos);
        if (times.Count % 2 != 0)
        {
            times.Add(Guid.Empty); // Dummy team for BYE
        }

        int numRodadas = times.Count - 1;
        int jogosPorRodada = times.Count / 2;

        var dataInicial = campeonato.DataInicio.ToUniversalTime();

        for (int r = 0; r < numRodadas; r++)
        {
            var rodada = new Rodada
            {
                Id = Guid.NewGuid(),
                CampeonatoId = campeonato.Id,
                Numero = r + 1
            };

            dbContext.Rodadas.Add(rodada);

            for (int j = 0; j < jogosPorRodada; j++)
            {
                var mandanteId = times[j];
                var visitanteId = times[times.Count - 1 - j];

                if (mandanteId != Guid.Empty && visitanteId != Guid.Empty)
                {
                    // Alternate home/away based on round to be fair
                    if (j == 0 && r % 2 != 0)
                    {
                        (mandanteId, visitanteId) = (visitanteId, mandanteId);
                    }

                    var partida = new Partida
                    {
                        Id = Guid.NewGuid(),
                        RodadaId = rodada.Id,
                        ClubeMandanteId = mandanteId,
                        ClubeVisitanteId = visitanteId,
                        DataHora = dataInicial.AddDays(r * 7), // 1 jogo por semana de placeholder
                        Local = campeonato.Local,
                        Status = StatusPartida.Agendada
                    };

                    dbContext.Partidas.Add(partida);
                }
            }

            // Rotate array (except the first element)
            var last = times.Last();
            times.RemoveAt(times.Count - 1);
            times.Insert(1, last);
        }

        campeonato.Status = StatusCampeonato.EmAndamento;
        campeonato.AtualizadoEm = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<bool>.Sucesso(true, "Calendário gerado com sucesso.");
    }
}
