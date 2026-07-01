using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Jogadores.Commands.AtualizarJogador;

public class AtualizarJogadorHandler(
    IJogadorRepositorio repositorio,
    IClubeRepositorio clubeRepositorio) 
    : IRequestHandler<AtualizarJogadorCommand>
{
    public async Task Handle(AtualizarJogadorCommand command, CancellationToken ct)
    {
        var jogador = await repositorio.ObterPorIdAsync(command.Id, ct);
        if (jogador == null)
            throw new ExcecaoNaoEncontrado("Jogador não encontrado.");

        if (command.ClubeId.HasValue)
        {
            var clube = await clubeRepositorio.ObterPorIdAsync(command.ClubeId.Value, ct);
            if (clube == null)
                throw new ExcecaoDominio("O clube informado não foi encontrado.");
        }

        jogador.CaminhoFoto = command.CaminhoFoto;
        jogador.NomeCompleto = command.NomeCompleto.Trim();
        jogador.DataNascimento = command.DataNascimento;
        jogador.PePreferencial = command.PePreferencial;
        jogador.Altura = command.Altura;
        jogador.Peso = command.Peso;
        jogador.PosicaoPrincipal = command.PosicaoPrincipal;
        jogador.PosicaoSecundaria = command.PosicaoSecundaria;
        jogador.BioHistorico = command.BioHistorico.Trim();
        jogador.ClubeId = command.ClubeId;
        jogador.AtualizadoEm = DateTime.UtcNow;

        repositorio.Atualizar(jogador);
        await repositorio.SalvarAsync(ct);
    }
}
