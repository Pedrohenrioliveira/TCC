using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Domain.Entidades;

namespace BaseApi.Domain.Interfaces.Repositorios;

/// <summary>
/// Contrato do repositório de jogadores.
/// </summary>
public interface IJogadorRepositorio
{
    Task<Jogador?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
    Task<(IEnumerable<Jogador> Itens, int Total)> ListarAsync(int pagina, int tamanhoPagina, string? busca, CancellationToken ct = default);
    Task AdicionarAsync(Jogador jogador, CancellationToken ct = default);
    void Atualizar(Jogador jogador);
    void Remover(Jogador jogador);
    Task SalvarAsync(CancellationToken ct = default);
}
