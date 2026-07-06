using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;

namespace BaseApi.Application.Jogadores.Commands.AtualizarJogador;

public class AtualizarFotoCommandHandler(IJogadorRepositorio repositorio) 
    : IRequestHandler<AtualizarFotoCommand, RespostaApi<Unit>>
{
    public async Task<RespostaApi<Unit>> Handle(AtualizarFotoCommand request, CancellationToken ct)
    {
        var jogador = await repositorio.ObterPorIdAsync(request.JogadorId, ct);
        if (jogador == null)
            return RespostaApi<Unit>.Falha("Jogador não encontrado.");

        jogador.CaminhoFoto = request.CaminhoFoto;
        jogador.AtualizadoEm = DateTime.UtcNow;

        repositorio.Atualizar(jogador);
        await repositorio.SalvarAsync(ct);

        return RespostaApi<Unit>.Sucesso(Unit.Value, "Foto atualizada com sucesso.");
    }
}
