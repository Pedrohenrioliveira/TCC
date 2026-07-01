using BaseApi.Domain.Entidades;
using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Jogadores.Commands.CriarJogador;

public class CriarJogadorHandler(
    IJogadorRepositorio repositorio,
    IClubeRepositorio clubeRepositorio) 
    : IRequestHandler<CriarJogadorCommand, CriarJogadorResposta>
{
    public async Task<CriarJogadorResposta> Handle(CriarJogadorCommand command, CancellationToken ct)
    {
        if (command.ClubeId.HasValue)
        {
            var clube = await clubeRepositorio.ObterPorIdAsync(command.ClubeId.Value, ct);
            if (clube == null)
                throw new ExcecaoDominio("O clube informado não foi encontrado.");
        }

        var jogador = new Jogador
        {
            CaminhoFoto = command.CaminhoFoto,
            NomeCompleto = command.NomeCompleto.Trim(),
            DataNascimento = command.DataNascimento,
            PePreferencial = command.PePreferencial,
            Altura = command.Altura,
            Peso = command.Peso,
            PosicaoPrincipal = command.PosicaoPrincipal,
            PosicaoSecundaria = command.PosicaoSecundaria,
            BioHistorico = command.BioHistorico.Trim(),
            ClubeId = command.ClubeId,
            CriadoEm = DateTime.UtcNow,
            AtualizadoEm = DateTime.UtcNow
        };

        await repositorio.AdicionarAsync(jogador, ct);
        await repositorio.SalvarAsync(ct);

        return new CriarJogadorResposta(jogador.Id, jogador.NomeCompleto, jogador.PosicaoPrincipal);
    }
}
