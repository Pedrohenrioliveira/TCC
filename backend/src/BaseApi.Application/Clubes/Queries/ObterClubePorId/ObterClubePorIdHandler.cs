using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using BaseApi.Application.Comum.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Clubes.Queries.ObterClubePorId;

public class ObterClubePorIdHandler(IClubeRepositorio repositorio, IAppDbContext context) 
    : IRequestHandler<ObterClubePorIdQuery, ClubeDetalheDto>
{
    public async Task<ClubeDetalheDto> Handle(ObterClubePorIdQuery query, CancellationToken ct)
    {
        var clube = await repositorio.ObterPorIdAsync(query.Id, ct);
        if (clube == null)
            throw new ExcecaoNaoEncontrado("Clube não encontrado.");

        var titulos = await context.Campeonatos
            .CountAsync(c => c.ClubeCampeaoId == clube.Id, ct);

        var dto = clube.Adapt<ClubeDetalheDto>();
        return dto with { TitulosOficiais = titulos };
    }
}
