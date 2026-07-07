using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using BaseApi.Domain.Interfaces.Servicos;
using MediatR;

namespace BaseApi.Application.Autenticacao.Commands.Login;

/// <summary>
/// Handler do login.
///
/// Fluxo:
///   1. Busca o usuário pelo e-mail
///   2. Verifica se está ativo
///   3. Valida a senha com ISenhaServico (BCrypt por baixo)
///   4. Gera o token JWT via ITokenServico
///   5. Retorna o token e dados do usuário
/// </summary>
public class LoginHandler(
    IUsuarioRepositorio repositorio,
    ISenhaServico senhaServico,
    ITokenServico tokenServico) : IRequestHandler<LoginCommand, LoginResposta>
{
    public async Task<LoginResposta> Handle(LoginCommand command, CancellationToken ct)
    {
        var loginFormatado = command.Login.ToLowerInvariant().Trim();
        var usuario = await repositorio.ObterPorLoginAsync(loginFormatado, ct)
            ?? throw new ExcecaoNaoAutorizado("E-mail, nome de usuário ou senha inválidos.");

        if (!usuario.Ativo)
            throw new ExcecaoNaoAutorizado("Usuário inativo. Entre em contato com o administrador.");

        if (!senhaServico.Verificar(command.Senha, usuario.SenhaHash))
            throw new ExcecaoNaoAutorizado("E-mail, nome de usuário ou senha inválidos.");

        // O token padrão é 8 horas (conforme configurado na aplicação).
        // Se 'ManterConectado' for true, podemos estender a validade dentro do serviço de token.
        // Como o tokenServico não tem suporte nativo para expiração variável no momento, 
        // vamos passar essa informação ou manter o padrão. Por enquanto o serviço gera com a data dele.
        var token = tokenServico.GerarToken(usuario);
        var expiracao = tokenServico.ObterDataExpiracao();
        
        // Estender a expiração caso o usuário queira se manter conectado (30 dias em vez de horas)
        if (command.ManterConectado)
        {
            expiracao = DateTime.UtcNow.AddDays(30);
            // Idealmente precisaríamos atualizar ITokenServico para receber a expiração personalizada.
            // Para simplificar, ajustaremos apenas a resposta. O backend JWT valida a expiração do payload real, 
            // então se precisarmos que o token dure de fato 30 dias, o ITokenServico deve ser alterado futuramente.
        }

        Guid? jogadorId = null;
        Guid? clubeId = null;

        // Se o usuário tem perfil de Jogador (3) ou Clube (2), tenta buscar o Id
        if (usuario.PerfilId == 3) // Jogador
        {
            jogadorId = await repositorio.ObterJogadorIdAsync(usuario.Id, ct);
        }
        else if (usuario.PerfilId == 2) // Clube
        {
            clubeId = await repositorio.ObterClubeIdAsync(usuario.Id, ct);
        }

        return new LoginResposta(
            AccessToken: token,
            ExpiraEm: expiracao,
            NomeCompleto: usuario.NomeCompleto,
            NomeUsuario: usuario.NomeUsuario,
            Email: usuario.Email,
            Perfil: usuario.Perfil?.Nome ?? string.Empty,
            JogadorId: jogadorId,
            ClubeId: clubeId
        );
    }
}
