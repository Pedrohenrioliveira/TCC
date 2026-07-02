using FluentValidation;

namespace BaseApi.Application.Autenticacao.Commands.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("O E-mail ou Nome de Usuário é obrigatório.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A Senha é obrigatória.");
    }
}
