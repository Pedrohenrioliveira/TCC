using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;

namespace BaseApi.Application.ClubRequests.Commands.AtualizarStatus;

public class AtualizarStatusSolicitacaoCommandHandler(ISolicitacaoClubeRepositorio repositorio) 
    : IRequestHandler<AtualizarStatusSolicitacaoCommand, RespostaApi<Unit>>
{
    public async Task<RespostaApi<Unit>> Handle(AtualizarStatusSolicitacaoCommand request, CancellationToken ct)
    {
        var solicitacao = await repositorio.ObterPorIdAsync(request.Id, ct);
        
        if (solicitacao == null)
            return RespostaApi<Unit>.Falha("Solicitação não encontrada.");

        solicitacao.Status = request.NovoStatus;
        solicitacao.DataResposta = DateTime.UtcNow;

        repositorio.Atualizar(solicitacao);
        await repositorio.SalvarAsync(ct);

        return RespostaApi<Unit>.Sucesso(Unit.Value, "Status da solicitação atualizado com sucesso.");
    }
}
