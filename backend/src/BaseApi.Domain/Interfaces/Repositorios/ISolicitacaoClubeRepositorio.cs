using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Domain.Entidades;

namespace BaseApi.Domain.Interfaces.Repositorios;

public interface ISolicitacaoClubeRepositorio
{
    Task<SolicitacaoClube?> ObterPorIdAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<SolicitacaoClube>> ListarPorJogadorAsync(Guid jogadorId, CancellationToken ct);
    Task<IEnumerable<SolicitacaoClube>> ListarPorClubeAsync(Guid clubeId, CancellationToken ct);
    void Adicionar(SolicitacaoClube solicitacao);
    void Atualizar(SolicitacaoClube solicitacao);
    Task SalvarAsync(CancellationToken ct);
}
