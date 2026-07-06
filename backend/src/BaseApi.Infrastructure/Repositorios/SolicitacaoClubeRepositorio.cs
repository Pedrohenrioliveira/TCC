using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Domain.Entidades;
using BaseApi.Domain.Interfaces.Repositorios;
using BaseApi.Infrastructure.Dados;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Infrastructure.Repositorios;

public class SolicitacaoClubeRepositorio(AppDbContext context) : ISolicitacaoClubeRepositorio
{
    public async Task<SolicitacaoClube?> ObterPorIdAsync(Guid id, CancellationToken ct)
    {
        return await context.SolicitacoesClubes
            .Include(s => s.Clube)
            .Include(s => s.Jogador)
            .FirstOrDefaultAsync(s => s.Id == id, ct);
    }

    public async Task<IEnumerable<SolicitacaoClube>> ListarPorJogadorAsync(Guid jogadorId, CancellationToken ct)
    {
        return await context.SolicitacoesClubes
            .Include(s => s.Clube)
            .Where(s => s.JogadorId == jogadorId)
            .OrderByDescending(s => s.DataSolicitacao)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<SolicitacaoClube>> ListarPorClubeAsync(Guid clubeId, CancellationToken ct)
    {
        return await context.SolicitacoesClubes
            .Include(s => s.Jogador)
            .Where(s => s.ClubeId == clubeId)
            .OrderByDescending(s => s.DataSolicitacao)
            .ToListAsync(ct);
    }

    public void Adicionar(SolicitacaoClube solicitacao)
    {
        context.SolicitacoesClubes.Add(solicitacao);
    }

    public void Atualizar(SolicitacaoClube solicitacao)
    {
        context.SolicitacoesClubes.Update(solicitacao);
    }

    public async Task SalvarAsync(CancellationToken ct)
    {
        await context.SaveChangesAsync(ct);
    }
}
