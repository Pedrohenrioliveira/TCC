using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;

namespace BaseApi.Application.Jogadores.Commands.AtualizarJogador;

public class AtualizarDadosPessoaisCommandHandler(IJogadorRepositorio repositorio) 
    : IRequestHandler<AtualizarDadosPessoaisCommand, RespostaApi<Unit>>
{
    public async Task<RespostaApi<Unit>> Handle(AtualizarDadosPessoaisCommand request, CancellationToken ct)
    {
        var jogador = await repositorio.ObterPorIdAsync(request.JogadorId, ct);
        if (jogador == null)
            return RespostaApi<Unit>.Falha("Jogador não encontrado.");

        jogador.NomeCompleto = request.NomeCompleto;
        jogador.DataNascimento = request.DataNascimento;
        jogador.BioHistorico = request.BioHistorico;
        jogador.AtualizadoEm = DateTime.UtcNow;

        repositorio.Atualizar(jogador);
        await repositorio.SalvarAsync(ct);

        return RespostaApi<Unit>.Sucesso(Unit.Value, "Dados pessoais atualizados com sucesso.");
    }
}
