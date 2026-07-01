using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Jogadores.Queries.ObterJogadorPorId;

public class ObterJogadorPorIdHandler(IJogadorRepositorio repositorio) 
    : IRequestHandler<ObterJogadorPorIdQuery, JogadorDetalheDto>
{
    public async Task<JogadorDetalheDto> Handle(ObterJogadorPorIdQuery query, CancellationToken ct)
    {
        var jogador = await repositorio.ObterPorIdAsync(query.Id, ct);
        if (jogador == null)
            throw new ExcecaoNaoEncontrado("Jogador não encontrado.");

        return jogador.Adapt<JogadorDetalheDto>();
    }
}
