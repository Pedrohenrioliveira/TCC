using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;

namespace BaseApi.Application.ClubRequests.Queries.ListarPorClube;

public class ListarSolicitacoesClubeQueryHandler(ISolicitacaoClubeRepositorio repositorio) 
    : IRequestHandler<ListarSolicitacoesClubeQuery, RespostaApi<IEnumerable<SolicitacaoParaClubeDto>>>
{
    public async Task<RespostaApi<IEnumerable<SolicitacaoParaClubeDto>>> Handle(ListarSolicitacoesClubeQuery request, CancellationToken ct)
    {
        var solicitacoes = await repositorio.ListarPorClubeAsync(request.ClubeId, ct);

        var dtos = solicitacoes.Select(s => new SolicitacaoParaClubeDto
        {
            Id = s.Id,
            JogadorId = s.JogadorId,
            NomeJogador = s.Jogador?.NomeCompleto ?? "Jogador Desconhecido",
            CaminhoFotoJogador = s.Jogador?.CaminhoFoto ?? "",
            Mensagem = s.Mensagem,
            Status = s.Status,
            DataSolicitacao = s.DataSolicitacao
        });

        return RespostaApi<IEnumerable<SolicitacaoParaClubeDto>>.Sucesso(dtos);
    }
}
