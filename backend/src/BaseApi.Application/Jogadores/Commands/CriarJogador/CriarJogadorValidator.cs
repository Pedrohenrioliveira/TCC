using FluentValidation;
using System;

namespace BaseApi.Application.Jogadores.Commands.CriarJogador;

public class CriarJogadorValidator : AbstractValidator<CriarJogadorCommand>
{
    public CriarJogadorValidator()
    {
        RuleFor(x => x.NomeCompleto)
            .NotEmpty().WithMessage("Nome completo é obrigatório.")
            .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres.");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("Data de nascimento é obrigatória.");

        RuleFor(x => x.PePreferencial)
            .IsInEnum().WithMessage("Pé preferencial inválido.");

        RuleFor(x => x.Altura)
            .InclusiveBetween(1, 300).WithMessage("Altura inválida.");

        RuleFor(x => x.Peso)
            .InclusiveBetween(1.0, 300.0).WithMessage("Peso inválido.");

        RuleFor(x => x.PosicaoPrincipal)
            .IsInEnum().WithMessage("Posição principal inválida.");

        RuleFor(x => x.PosicaoSecundaria)
            .IsInEnum().WithMessage("Posição secundária inválida.")
            .When(x => x.PosicaoSecundaria.HasValue);

        RuleFor(x => x.BioHistorico)
            .MaximumLength(1000).WithMessage("Bio deve ter no máximo 1000 caracteres.");
    }
}
