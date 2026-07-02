using MediatR;

namespace BaseApi.Application.Autenticacao.Commands.Login;

/// <summary>
/// Command de login. Recebe e-mail ou nome de usuário e senha.
/// </summary>
public record LoginCommand(string Login, string Senha, bool ManterConectado = false) : IRequest<LoginResposta>;

/// <summary>
/// Resposta do login com o token JWT e sua expiração.
/// O cliente deve enviar este token no header: Authorization: Bearer {AccessToken}
/// </summary>
public record LoginResposta(
    string AccessToken,
    DateTime ExpiraEm,
    string NomeCompleto,
    string NomeUsuario,
    string Email,
    string Perfil,
    Guid? JogadorId = null,
    Guid? ClubeId = null
);
