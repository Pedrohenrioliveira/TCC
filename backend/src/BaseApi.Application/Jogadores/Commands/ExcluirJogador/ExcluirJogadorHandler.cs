using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Jogadores.Commands.ExcluirJogador;

public class ExcluirJogadorHandler(IJogadorRepositorio repositorio) 
    : IRequestHandler<ExcluirJogadorCommand>
{
    public async Task Handle(ExcluirJogadorCommand command, CancellationToken ct)
    {
        var jogador = await repositorio.ObterPorIdAsync(command.Id, ct);
        if (jogador == null)
            throw new ExcecaoNaoEncontrado("Jogador não encontrado.");

        repositorio.Remover(jogador);
        await repositorio.SalvarAsync(ct);
    }
}
