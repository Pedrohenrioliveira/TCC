using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Campeonatos.Commands.AtualizarStatusCampeonato;

public class AtualizarStatusCampeonatoCommand : IRequest<RespostaApi<bool>>
{
    public Guid CampeonatoId { get; set; }
    public StatusCampeonato NovoStatus { get; set; }
}

public class AtualizarStatusCampeonatoCommandHandler(IAppDbContext dbContext) : IRequestHandler<AtualizarStatusCampeonatoCommand, RespostaApi<bool>>
{
    public async Task<RespostaApi<bool>> Handle(AtualizarStatusCampeonatoCommand request, CancellationToken cancellationToken)
    {
        var campeonato = await dbContext.Campeonatos.FirstOrDefaultAsync(c => c.Id == request.CampeonatoId, cancellationToken);
        
        if (campeonato == null)
            return RespostaApi<bool>.Falha("Campeonato não encontrado.");

        campeonato.Status = request.NovoStatus;
        campeonato.AtualizadoEm = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<bool>.Sucesso(true, "Status do campeonato atualizado com sucesso.");
    }
}
