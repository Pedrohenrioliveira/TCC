using BaseApi.Domain.Interfaces.Repositorios;
using FluentValidation;
using System;

namespace BaseApi.Application.Clubes.Commands.CriarClube;

public class CriarClubeValidator : AbstractValidator<CriarClubeCommand>
{
    public CriarClubeValidator(IClubeRepositorio repositorio)
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome do clube é obrigatório.")
            .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres.");

        RuleFor(x => x.AnoFundacao)
            .InclusiveBetween(1800, DateTime.UtcNow.Year + 1)
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
