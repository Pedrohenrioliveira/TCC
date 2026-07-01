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
/// Implementação do repositório de jogadores usando Entity Framework Core.
/// </summary>
public class JogadorRepositorio(AppDbContext contexto) : IJogadorRepositorio
{
    public async Task<Jogador?> ObterPorIdAsync(Guid id, CancellationToken ct = default)
        => await contexto.Set<Jogador>()
            .Include(j => j.Clube)
            .FirstOrDefaultAsync(j => j.Id == id, ct);

    public async Task<(IEnumerable<Jogador> Itens, int Total)> ListarAsync(
        int pagina, int tamanhoPagina, string? busca, CancellationToken ct = default)
    {
        var query = contexto.Set<Jogador>()
            .Include(j => j.Clube)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            busca = busca.ToLower();
            query = query.Where(j =>
                j.NomeCompleto.ToLower().Contains(busca) ||
                (j.Clube != null && j.Clube.Nome.ToLower().Contains(busca)));
        }

        var total = await query.CountAsync(ct);
        var itens = await query
            .OrderBy(j => j.NomeCompleto)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync(ct);

        return (itens, total);
    }

    public async Task AdicionarAsync(Jogador jogador, CancellationToken ct = default)
        => await contexto.Set<Jogador>().AddAsync(jogador, ct);

    public void Atualizar(Jogador jogador)
        => contexto.Set<Jogador>().Update(jogador);

    public void Remover(Jogador jogador)
        => contexto.Set<Jogador>().Remove(jogador);

    public async Task SalvarAsync(CancellationToken ct = default)
        => await contexto.SaveChangesAsync(ct);
}
