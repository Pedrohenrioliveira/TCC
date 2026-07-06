using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;

namespace BaseApi.Application.ClubRequests.Queries.Listar;

public class ListarSolicitacoesJogadorQueryHandler(ISolicitacaoClubeRepositorio repositorio) 
    : IRequestHandler<ListarSolicitacoesJogadorQuery, RespostaApi<IEnumerable<SolicitacaoDto>>>
{
    public async Task<RespostaApi<IEnumerable<SolicitacaoDto>>> Handle(ListarSolicitacoesJogadorQuery request, CancellationToken ct)
    {
        var solicitacoes = await repositorio.ListarPorJogadorAsync(request.JogadorId, ct);

        var dtos = solicitacoes.Select(s => new SolicitacaoDto
        {
            Id = s.Id,
            ClubeId = s.ClubeId,
            NomeClube = s.Clube?.Nome ?? "Clube Desconhecido",
            EscudoClube = s.Clube?.CaminhoEscudo ?? "",
            Mensagem = s.Mensagem,
            Status = s.Status,
            DataSolicitacao = s.DataSolicitacao
        });

        return RespostaApi<IEnumerable<SolicitacaoDto>>.Sucesso(dtos);
    }
}
