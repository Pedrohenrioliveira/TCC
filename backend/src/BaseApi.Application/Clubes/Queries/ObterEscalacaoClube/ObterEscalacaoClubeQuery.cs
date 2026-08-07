using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Clubes.Queries.ObterEscalacaoClube;

public record ObterEscalacaoClubeQuery(Guid ClubeId) : IRequest<string?>;

public class ObterEscalacaoClubeHandler(IClubeRepositorio repositorio)
    : IRequestHandler<ObterEscalacaoClubeQuery, string?>
{
    public async Task<string?> Handle(ObterEscalacaoClubeQuery request, CancellationToken ct)
    {
        var clube = await repositorio.ObterPorIdAsync(request.ClubeId, ct);
        
        if (clube == null)
            throw new ExcecaoNaoEncontrado("Clube não encontrado.");

        return clube.EscalacaoJson;
    }
}
