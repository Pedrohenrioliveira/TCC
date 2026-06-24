using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Domain.Entidades;

namespace BaseApi.Domain.Interfaces.Repositorios;

/// <summary>
/// Contrato do repositório de clubes.
/// </summary>
public interface IClubeRepositorio
{
    Task<Clube?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
    Task<(IEnumerable<Clube> Itens, int Total)> ListarAsync(int pagina, int tamanhoPagina, string? busca, CancellationToken ct = default);
    Task<bool> NomeExisteAsync(string nome, Guid? ignorarId = null, CancellationToken ct = default);
    Task AdicionarAsync(Clube clube, CancellationToken ct = default);
    void Atualizar(Clube clube);
    void Remover(Clube clube);
    Task SalvarAsync(CancellationToken ct = default);
}
