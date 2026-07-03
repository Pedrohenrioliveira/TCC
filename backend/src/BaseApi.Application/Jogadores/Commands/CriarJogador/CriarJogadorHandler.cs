using BaseApi.Domain.Entidades;
using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using BaseApi.Domain.Interfaces.Servicos;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Jogadores.Commands.CriarJogador;

public class CriarJogadorHandler(
    IJogadorRepositorio repositorio,
    IClubeRepositorio clubeRepositorio,
    IUsuarioRepositorio usuarioRepositorio,
    ISenhaServico senhaServico) 
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

        // 1. Criar o usuário para o Jogador
        var usuario = new Usuario
        {
            NomeCompleto = command.NomeCompleto.Trim(),
            Email = command.Email.ToLowerInvariant().Trim(),
            SenhaHash = senhaServico.GerarHash(command.Senha),
            PerfilId = 3, // Perfil de Jogador / Usuário
            Ativo = true,
            CriadoEm = DateTime.UtcNow,
            AtualizadoEm = DateTime.UtcNow
        };

        await usuarioRepositorio.AdicionarAsync(usuario, ct);

        // 2. Criar o Perfil de Jogador
        var jogador = new Jogador
        {
            UsuarioId = usuario.Id,
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
        
        // 3. Salvar tudo no banco de dados (EF Core cuidará da transação)
        await repositorio.SalvarAsync(ct);

        return new CriarJogadorResposta(jogador.Id, jogador.NomeCompleto, jogador.PosicaoPrincipal);
    }
}
