using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;

namespace BaseApi.Application.ClubRequests.Commands.Criar;

public class CriarSolicitacaoCommandHandler(ISolicitacaoClubeRepositorio repositorio) 
    : IRequestHandler<CriarSolicitacaoCommand, RespostaApi<Guid>>
{
    public async Task<RespostaApi<Guid>> Handle(CriarSolicitacaoCommand request, CancellationToken ct)
    {
        var solicitacao = new SolicitacaoClube
        {
            JogadorId = request.JogadorId,
            ClubeId = request.ClubeId,
            Mensagem = request.Mensagem,
            Status = StatusSolicitacao.Pendente,
            DataSolicitacao = DateTime.UtcNow
        };

        repositorio.Adicionar(solicitacao);
        await repositorio.SalvarAsync(ct);

        return RespostaApi<Guid>.Sucesso(solicitacao.Id, "Solicitação enviada com sucesso.");
    }
}
