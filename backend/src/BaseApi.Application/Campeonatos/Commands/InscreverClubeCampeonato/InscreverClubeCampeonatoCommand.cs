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

        var inscricaoExistente = await dbContext.Classificacoes
            .AnyAsync(c => c.CampeonatoId == request.CampeonatoId && c.ClubeId == request.ClubeId, cancellationToken);

        if (inscricaoExistente)
            return RespostaApi<bool>.Falha("O clube já está inscrito neste campeonato.");

        // Ao inscrever o clube, inicializamos sua linha na tabela de classificação com zero
        var novaClassificacao = new Classificacao
        {
            CampeonatoId = request.CampeonatoId,
            ClubeId = request.ClubeId,
            Pontos = 0,
            PartidasJogadas = 0,
            Vitorias = 0,
            Empates = 0,
            Derrotas = 0,
            GolsPro = 0,
            GolsContra = 0
        };

        dbContext.Classificacoes.Add(novaClassificacao);
        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<bool>.Sucesso(true, "Clube inscrito com sucesso e tabela de classificação inicializada.");
    }
}
