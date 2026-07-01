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
            .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
            .LessThan(DateTime.UtcNow.AddYears(-5)).WithMessage("O atleta deve ter pelo menos 5 anos de idade.")
            .GreaterThan(DateTime.UtcNow.AddYears(-100)).WithMessage("Data de nascimento inválida.");

        RuleFor(x => x.PePreferencial)
            .IsInEnum().WithMessage("Pé preferencial inválido.");

        RuleFor(x => x.Altura)
            .InclusiveBetween(50, 250).WithMessage("Altura deve estar entre 50cm e 250cm.");

        RuleFor(x => x.Peso)
            .InclusiveBetween(20.0, 200.0).WithMessage("Peso deve estar entre 20kg e 200kg.");

        RuleFor(x => x.PosicaoPrincipal)
            .IsInEnum().WithMessage("Posição principal inválida.");

        RuleFor(x => x.PosicaoSecundaria)
            .IsInEnum().WithMessage("Posição secundária inválida.")
            .When(x => x.PosicaoSecundaria.HasValue);

        RuleFor(x => x.BioHistorico)
            .NotEmpty().WithMessage("Bio e histórico no futebol são obrigatórios.")
            .MaximumLength(1000).WithMessage("Bio deve ter no máximo 1000 caracteres.");
    }
}
