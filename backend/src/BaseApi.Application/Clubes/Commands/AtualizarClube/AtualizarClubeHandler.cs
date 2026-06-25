using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Clubes.Commands.AtualizarClube;

public class AtualizarClubeHandler(IClubeRepositorio repositorio) 
    : IRequestHandler<AtualizarClubeCommand>
{
    public async Task Handle(AtualizarClubeCommand command, CancellationToken ct)
    {
        var clube = await repositorio.ObterPorIdAsync(command.Id, ct);
        if (clube == null)
            throw new ExcecaoNaoEncontrado("Clube não encontrado.");

        var nomeExiste = await repositorio.NomeExisteAsync(command.Nome, command.Id, ct);
        if (nomeExiste)
            throw new ExcecaoDominio("Já existe um clube cadastrado com este nome.");

        clube.CaminhoEscudo = command.CaminhoEscudo;
        clube.Nome = command.Nome.Trim();
        clube.AnoFundacao = command.AnoFundacao;
        clube.CidadeEstado = command.CidadeEstado.Trim();
        clube.LigaCompeticao = command.LigaCompeticao.Trim();
        clube.EstadioPrincipal = command.EstadioPrincipal?.Trim();
        clube.BreveHistoria = command.BreveHistoria.Trim();
        clube.AtualizadoEm = DateTime.UtcNow;

        repositorio.Atualizar(clube);
        await repositorio.SalvarAsync(ct);
    }
}
