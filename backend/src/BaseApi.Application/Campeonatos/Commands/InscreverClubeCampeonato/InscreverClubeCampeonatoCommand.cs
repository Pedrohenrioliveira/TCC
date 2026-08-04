using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Campeonatos.Commands.InscreverClubeCampeonato;

public class InscreverClubeCampeonatoCommand : IRequest<RespostaApi<bool>>
{
    public Guid CampeonatoId { get; set; }
    public Guid ClubeId { get; set; }
    public bool AceitouRegulamento { get; set; }
}

public class InscreverClubeCampeonatoCommandHandler(IAppDbContext dbContext) : IRequestHandler<InscreverClubeCampeonatoCommand, RespostaApi<bool>>
{
    public async Task<RespostaApi<bool>> Handle(InscreverClubeCampeonatoCommand request, CancellationToken cancellationToken)
    {
        var campeonato = await dbContext.Campeonatos.FirstOrDefaultAsync(c => c.Id == request.CampeonatoId, cancellationToken);
        if (campeonato == null)
            return RespostaApi<bool>.Falha("Campeonato não encontrado.");

        var clube = await dbContext.Clubes.FirstOrDefaultAsync(c => c.Id == request.ClubeId, cancellationToken);
        if (clube == null)
            return RespostaApi<bool>.Falha("Clube não encontrado.");

        var jaInscritoOuPendente = await dbContext.InscricoesCampeonatos
            .AnyAsync(i => i.CampeonatoId == request.CampeonatoId && i.ClubeId == request.ClubeId, cancellationToken);

        if (jaInscritoOuPendente)
            return RespostaApi<bool>.Falha("O clube já solicitou inscrição ou está inscrito neste campeonato.");

        if (!request.AceitouRegulamento)
            return RespostaApi<bool>.Falha("O clube deve aceitar o regulamento para solicitar a inscrição.");

        var inscricao = new InscricaoCampeonato
        {
            CampeonatoId = request.CampeonatoId,
            ClubeId = request.ClubeId,
            AceitouRegulamento = request.AceitouRegulamento,
            Status = StatusInscricao.Pendente,
            DataSolicitacao = DateTime.UtcNow
        };

        dbContext.InscricoesCampeonatos.Add(inscricao);
        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<bool>.Sucesso(true, "Solicitação de inscrição enviada com sucesso e aguardando aprovação.");
    }
}
