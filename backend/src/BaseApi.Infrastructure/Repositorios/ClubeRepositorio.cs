using BaseApi.Domain.Entidades;
using BaseApi.Domain.Interfaces.Repositorios;
using BaseApi.Infrastructure.Dados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Infrastructure.Repositorios;

/// <summary>
/// Implementação do repositório de clubes usando Entity Framework Core.
/// </summary>
public class ClubeRepositorio(AppDbContext contexto) : IClubeRepositorio
{
    public async Task<Clube?> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        => await contexto.Set<Clube>().FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<(IEnumerable<Clube> Itens, int Total)> ListarAsync(
        int pagina, int tamanhoPagina, string? busca, CancellationToken ct = default)
    {
        var query = contexto.Set<Clube>().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            busca = busca.ToLower();
            query = query.Where(c =>
                c.Nome.ToLower().Contains(busca) ||
                c.CidadeEstado.ToLower().Contains(busca) ||
                c.LigaCompeticao.ToLower().Contains(busca));
        }

        var total = await query.CountAsync(ct);
        var itens = await query
            .OrderBy(c => c.Nome)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync(ct);

        return (itens, total);
    }

    public async Task<bool> NomeExisteAsync(string nome, Guid? ignorarId = null, CancellationToken ct = default)
        => await contexto.Set<Clube>().AnyAsync(c =>
            c.Nome == nome && (ignorarId == null || c.Id != ignorarId), ct);

    public async Task AdicionarAsync(Clube clube, CancellationToken ct = default)
        => await contexto.Set<Clube>().AddAsync(clube, ct);

    public void Atualizar(Clube clube)
        => contexto.Set<Clube>().Update(clube);

    public void Remover(Clube clube)
        => contexto.Set<Clube>().Remove(clube);

    public async Task SalvarAsync(CancellationToken ct = default)
        => await contexto.SaveChangesAsync(ct);
}
