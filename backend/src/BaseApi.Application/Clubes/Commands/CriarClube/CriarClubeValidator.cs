using BaseApi.Domain.Interfaces.Repositorios;
using FluentValidation;
using System;

namespace BaseApi.Application.Clubes.Commands.CriarClube;

public class CriarClubeValidator : AbstractValidator<CriarClubeCommand>
{
    public CriarClubeValidator(IClubeRepositorio repositorio)
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome do clube é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do clube deve ter no máximo 100 caracteres.");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado é inválido.");

        RuleFor(c => c.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.");

        RuleFor(c => c.AnoFundacao)
            .GreaterThan(0)
            .WithMessage("Ano de fundação inválido.");

        RuleFor(x => x.CidadeEstado)
            .NotEmpty().WithMessage("Cidade/Estado é obrigatório.")
            .MaximumLength(100).WithMessage("Cidade/Estado deve ter no máximo 100 caracteres.");

        RuleFor(x => x.LigaCompeticao)
            .NotEmpty().WithMessage("Liga/Competição atual é obrigatória.")
            .MaximumLength(150).WithMessage("Liga/Competição deve ter no máximo 150 caracteres.");

        RuleFor(x => x.EstadioPrincipal)
            .MaximumLength(150).WithMessage("Estádio principal deve ter no máximo 150 caracteres.");

        RuleFor(x => x.BreveHistoria)
            .NotEmpty().WithMessage("Breve história do clube é obrigatória.")
            .MaximumLength(1000).WithMessage("Breve história deve ter no máximo 1000 caracteres.");
    }
}
