using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Clubes.Commands.ExcluirClube;

public class ExcluirClubeHandler(IClubeRepositorio repositorio) 
    : IRequestHandler<ExcluirClubeCommand>
{
    public async Task Handle(ExcluirClubeCommand command, CancellationToken ct)
    {
        var clube = await repositorio.ObterPorIdAsync(command.Id, ct);
        if (clube == null)
            throw new ExcecaoNaoEncontrado("Clube não encontrado.");

        repositorio.Remover(clube);
        await repositorio.SalvarAsync(ct);
    }
}
