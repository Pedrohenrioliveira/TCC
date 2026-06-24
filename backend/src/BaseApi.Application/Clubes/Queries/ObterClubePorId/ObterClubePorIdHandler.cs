using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Clubes.Queries.ObterClubePorId;

public class ObterClubePorIdHandler(IClubeRepositorio repositorio) 
    : IRequestHandler<ObterClubePorIdQuery, ClubeDetalheDto>
{
    public async Task<ClubeDetalheDto> Handle(ObterClubePorIdQuery query, CancellationToken ct)
    {
        var clube = await repositorio.ObterPorIdAsync(query.Id, ct);
        if (clube == null)
            throw new ExcecaoNaoEncontrado("Clube não encontrado.");

        return clube.Adapt<ClubeDetalheDto>();
    }
}
