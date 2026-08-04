using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Campeonatos.Commands.ProcessarInscricaoCampeonato;

public class ProcessarInscricaoCampeonatoCommand : IRequest<RespostaApi<bool>>
{
    public Guid InscricaoId { get; set; }
    public bool Aprovar { get; set; }
}

public class ProcessarInscricaoCampeonatoCommandHandler(IAppDbContext dbContext) : IRequestHandler<ProcessarInscricaoCampeonatoCommand, RespostaApi<bool>>
{
    public async Task<RespostaApi<bool>> Handle(ProcessarInscricaoCampeonatoCommand request, CancellationToken cancellationToken)
    {
        var inscricao = await dbContext.InscricoesCampeonatos
            .FirstOrDefaultAsync(i => i.Id == request.InscricaoId, cancellationToken);
            
        if (inscricao == null)
            return RespostaApi<bool>.Falha("Inscrição não encontrada.");

        if (inscricao.Status != StatusInscricao.Pendente)
            return RespostaApi<bool>.Falha("Esta inscrição já foi processada.");

        if (request.Aprovar)
        {
            inscricao.Status = StatusInscricao.Aprovada;
            inscricao.DataResposta = DateTime.UtcNow;

            var novaClassificacao = new Classificacao
            {
                CampeonatoId = inscricao.CampeonatoId,
                ClubeId = inscricao.ClubeId,
                Pontos = 0,
                PartidasJogadas = 0,
                Vitorias = 0,
                Empates = 0,
                Derrotas = 0,
                GolsPro = 0,
                GolsContra = 0
            };

            dbContext.Classificacoes.Add(novaClassificacao);
        }
        else
        {
            inscricao.Status = StatusInscricao.Rejeitada;
            inscricao.DataResposta = DateTime.UtcNow;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        var acao = request.Aprovar ? "aprovada" : "rejeitada";
        return RespostaApi<bool>.Sucesso(true, $"Inscrição {acao} com sucesso.");
    }
}
