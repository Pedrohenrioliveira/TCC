using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using BaseApi.Application.Comum.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Jogadores.Queries.ObterJogadorPorId;

public class ObterJogadorPorIdHandler(IJogadorRepositorio repositorio, IAppDbContext context) 
    : IRequestHandler<ObterJogadorPorIdQuery, JogadorDetalheDto>
{
    public async Task<JogadorDetalheDto> Handle(ObterJogadorPorIdQuery query, CancellationToken ct)
    {
        var jogador = await repositorio.ObterPorIdAsync(query.Id, ct);
        if (jogador == null)
            throw new ExcecaoNaoEncontrado("Jogador não encontrado.");

        int titulos = 0;
        if (jogador.ClubeId != null)
        {
            titulos = await context.Campeonatos
                .CountAsync(c => c.ClubeCampeaoId == jogador.ClubeId, ct);
        }

        var dto = jogador.Adapt<JogadorDetalheDto>();
        return dto with { TitulosOficiais = titulos, GolsOficiais = 0 }; // 0 gols oficias ate sumula
    }
}
