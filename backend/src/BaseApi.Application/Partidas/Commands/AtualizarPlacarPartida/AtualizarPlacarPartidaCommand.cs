using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Partidas.Commands.AtualizarPlacarPartida;

public class AtualizarPlacarPartidaCommand : IRequest<RespostaApi<bool>>
{
    public Guid PartidaId { get; set; }
    public int GolsMandante { get; set; }
    public int GolsVisitante { get; set; }
}

public class AtualizarPlacarPartidaCommandHandler(IAppDbContext dbContext) : IRequestHandler<AtualizarPlacarPartidaCommand, RespostaApi<bool>>
{
    public async Task<RespostaApi<bool>> Handle(AtualizarPlacarPartidaCommand request, CancellationToken cancellationToken)
    {
        var partida = await dbContext.Partidas
            .Include(p => p.Rodada)
            .FirstOrDefaultAsync(p => p.Id == request.PartidaId, cancellationToken);

        if (partida == null)
            return RespostaApi<bool>.Falha("Partida não encontrada.");

        if (partida.Status == StatusPartida.Finalizada)
            return RespostaApi<bool>.Falha("A partida já foi finalizada. Edições não são permitidas por segurança.");

        var campeonatoId = partida.Rodada.CampeonatoId;

        var classifMandante = await dbContext.Classificacoes
            .FirstOrDefaultAsync(c => c.CampeonatoId == campeonatoId && c.ClubeId == partida.ClubeMandanteId, cancellationToken);
            
        var classifVisitante = await dbContext.Classificacoes
            .FirstOrDefaultAsync(c => c.CampeonatoId == campeonatoId && c.ClubeId == partida.ClubeVisitanteId, cancellationToken);

        if (classifMandante == null || classifVisitante == null)
            return RespostaApi<bool>.Falha("Um ou ambos os clubes não estão com a tabela de classificação inicializada.");

        // Atualizar placar e status
        partida.GolsMandante = request.GolsMandante;
        partida.GolsVisitante = request.GolsVisitante;
        partida.Status = StatusPartida.Finalizada;
        partida.AtualizadoEm = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<bool>.Sucesso(true, "Placar atualizado com sucesso. (Pontuação deve ser gerida manualmente)");
    }
}
