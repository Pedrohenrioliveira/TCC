using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Jogadores.Queries.ObterHomeDashboard;

public class ObterHomeDashboardHandler(IJogadorRepositorio repositorio)
    : IRequestHandler<ObterHomeDashboardQuery, HomeDashboardDto>
{
    public async Task<HomeDashboardDto> Handle(ObterHomeDashboardQuery query, CancellationToken ct)
    {
        var jogador = await repositorio.ObterPorIdAsync(query.Id, ct);
        
        if (jogador == null)
            throw new ExcecaoNaoEncontrado("Jogador não encontrado.");

        // Dados mockados para estatísticas (a serem implementados no futuro)
        return new HomeDashboardDto(
            JogadorId: jogador.Id,
            CaminhoFoto: jogador.CaminhoFoto,
            NomeCompleto: jogador.NomeCompleto,
            PosicaoPrincipal: jogador.PosicaoPrincipal.ToString(),
            Nivel: 42,
            GolsNaTemporada: 14,
            Assistencias: 8,
            VariacaoGols: 12.0m,
            VariacaoAssistencias: 5.0m
        );
    }
}
