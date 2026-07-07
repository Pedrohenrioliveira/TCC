using BaseApi.Domain.Entidades;
using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using BaseApi.Domain.Interfaces.Servicos;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Clubes.Commands.CriarClube;

public class CriarClubeHandler(
    IClubeRepositorio repositorio,
    IUsuarioRepositorio usuarioRepositorio,
    ISenhaServico senhaServico) 
    : IRequestHandler<CriarClubeCommand, CriarClubeResposta>
{
    public async Task<CriarClubeResposta> Handle(CriarClubeCommand command, CancellationToken ct)
    {
        var nomeExiste = await repositorio.NomeExisteAsync(command.Nome, ct: ct);
        if (nomeExiste)
            throw new ExcecaoDominio("Já existe um clube cadastrado com este nome.");

        var emailExiste = await usuarioRepositorio.EmailExisteAsync(command.Email.ToLowerInvariant().Trim(), null, ct);
        if (emailExiste)
            throw new ExcecaoDominio("Já existe um usuário cadastrado com este e-mail.");

        var usuario = new Usuario
        {
            NomeCompleto = command.Nome.Trim(),
            NomeUsuario = command.Email.ToLowerInvariant().Trim(),
            Email = command.Email.ToLowerInvariant().Trim(),
            SenhaHash = senhaServico.GerarHash(command.Senha),
            PerfilId = 2, // Perfil de Clube
            Ativo = true,
            CriadoEm = DateTime.UtcNow,
            AtualizadoEm = DateTime.UtcNow
        };

        await usuarioRepositorio.AdicionarAsync(usuario, ct);

        var clube = new Clube
        {
            UsuarioId = usuario.Id,
            CaminhoEscudo = command.CaminhoEscudo,
            Nome = command.Nome.Trim(),
            AnoFundacao = command.AnoFundacao,
            CidadeEstado = command.CidadeEstado.Trim(),
            LigaCompeticao = command.LigaCompeticao.Trim(),
            EstadioPrincipal = command.EstadioPrincipal?.Trim(),
            BreveHistoria = command.BreveHistoria.Trim(),
            CriadoEm = DateTime.UtcNow,
            AtualizadoEm = DateTime.UtcNow
        };

        await repositorio.AdicionarAsync(clube, ct);
        await repositorio.SalvarAsync(ct);

        return new CriarClubeResposta(clube.Id, clube.Nome, clube.CidadeEstado);
    }
}
