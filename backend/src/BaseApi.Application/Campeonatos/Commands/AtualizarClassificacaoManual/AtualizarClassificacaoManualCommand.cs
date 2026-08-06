using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Campeonatos.Commands.AtualizarClassificacaoManual;

public class AtualizarClassificacaoManualCommand : IRequest<RespostaApi<bool>>
{
    public Guid CampeonatoId { get; set; }
    public Guid ClubeId { get; set; }
    public int Pontos { get; set; }
    public int PartidasJogadas { get; set; }
    public int Vitorias { get; set; }
    public int Empates { get; set; }
    public int Derrotas { get; set; }
    public int GolsPro { get; set; }
    public int GolsContra { get; set; }
}

public class AtualizarClassificacaoManualCommandHandler(IAppDbContext dbContext) : IRequestHandler<AtualizarClassificacaoManualCommand, RespostaApi<bool>>
{
    public async Task<RespostaApi<bool>> Handle(AtualizarClassificacaoManualCommand request, CancellationToken cancellationToken)
    {
        var classif = await dbContext.Classificacoes
            .FirstOrDefaultAsync(c => c.CampeonatoId == request.CampeonatoId && c.ClubeId == request.ClubeId, cancellationToken);

        if (classif == null)
            return RespostaApi<bool>.Falha("Classificação não encontrada para o clube neste campeonato.");

        classif.Pontos = request.Pontos;
        classif.PartidasJogadas = request.PartidasJogadas;
        classif.Vitorias = request.Vitorias;
        classif.Empates = request.Empates;
        classif.Derrotas = request.Derrotas;
        classif.GolsPro = request.GolsPro;
        classif.GolsContra = request.GolsContra;
        classif.AtualizadoEm = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<bool>.Sucesso(true, "Classificação atualizada manualmente com sucesso.");
    }
}
