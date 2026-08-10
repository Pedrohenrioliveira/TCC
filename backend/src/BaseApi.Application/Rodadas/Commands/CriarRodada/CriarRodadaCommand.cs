using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Rodadas.Commands.CriarRodada;

public class CriarRodadaCommand : IRequest<RespostaApi<Guid>>
{
    public Guid CampeonatoId { get; set; }
    public int Numero { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
}

public class CriarRodadaCommandHandler(IAppDbContext dbContext) : IRequestHandler<CriarRodadaCommand, RespostaApi<Guid>>
{
    public async Task<RespostaApi<Guid>> Handle(CriarRodadaCommand request, CancellationToken cancellationToken)
    {
        var campeonato = await dbContext.Campeonatos.FirstOrDefaultAsync(c => c.Id == request.CampeonatoId, cancellationToken);
        if (campeonato == null)
            return RespostaApi<Guid>.Falha("Campeonato não encontrado.");

        var rodada = new Rodada
        {
            CampeonatoId = request.CampeonatoId,
            Numero = request.Numero,
            Nome = request.Nome,
            DataInicio = request.DataInicio,
            DataFim = request.DataFim
        };

        dbContext.Rodadas.Add(rodada);
        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<Guid>.Sucesso(rodada.Id, "Rodada criada com sucesso.");
    }
}
