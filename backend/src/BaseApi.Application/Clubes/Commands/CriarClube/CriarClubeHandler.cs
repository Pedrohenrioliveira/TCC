using BaseApi.Domain.Entidades;
using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Clubes.Commands.CriarClube;

public class CriarClubeHandler(IClubeRepositorio repositorio) 
    : IRequestHandler<CriarClubeCommand, CriarClubeResposta>
{
    public async Task<CriarClubeResposta> Handle(CriarClubeCommand command, CancellationToken ct)
    {
        var nomeExiste = await repositorio.NomeExisteAsync(command.Nome, ct: ct);
        if (nomeExiste)
            throw new ExcecaoDominio("Já existe um clube cadastrado com este nome.");

        var clube = new Clube
        {
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
