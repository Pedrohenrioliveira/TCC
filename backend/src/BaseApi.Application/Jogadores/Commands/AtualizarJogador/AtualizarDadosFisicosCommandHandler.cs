using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;

namespace BaseApi.Application.Jogadores.Commands.AtualizarJogador;

public class AtualizarDadosFisicosCommandHandler(IJogadorRepositorio repositorio) 
    : IRequestHandler<AtualizarDadosFisicosCommand, RespostaApi<Unit>>
{
    public async Task<RespostaApi<Unit>> Handle(AtualizarDadosFisicosCommand request, CancellationToken ct)
    {
        var jogador = await repositorio.ObterPorIdAsync(request.JogadorId, ct);
        if (jogador == null)
            return RespostaApi<Unit>.Falha("Jogador não encontrado.");

        jogador.PePreferencial = request.PePreferencial;
        jogador.Altura = request.Altura;
        jogador.Peso = request.Peso;
        jogador.PosicaoPrincipal = request.PosicaoPrincipal;
        jogador.PosicaoSecundaria = request.PosicaoSecundaria;
        jogador.AtualizadoEm = DateTime.UtcNow;

        repositorio.Atualizar(jogador);
        await repositorio.SalvarAsync(ct);

        return RespostaApi<Unit>.Sucesso(Unit.Value, "Dados físicos atualizados com sucesso.");
    }
}
