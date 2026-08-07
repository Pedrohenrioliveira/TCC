using BaseApi.Domain.Excecoes;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Clubes.Commands.AtualizarEscalacaoClube;

public record AtualizarEscalacaoClubeCommand(
    Guid ClubeId,
    string EscalacaoJson
) : IRequest<bool>;

public class AtualizarEscalacaoClubeHandler(IClubeRepositorio repositorio)
    : IRequestHandler<AtualizarEscalacaoClubeCommand, bool>
{
    public async Task<bool> Handle(AtualizarEscalacaoClubeCommand request, CancellationToken ct)
    {
        var clube = await repositorio.ObterPorIdAsync(request.ClubeId, ct);
        
        if (clube == null)
            throw new ExcecaoNaoEncontrado("Clube não encontrado.");

        clube.EscalacaoJson = request.EscalacaoJson;
        
        repositorio.Atualizar(clube);
        await repositorio.SalvarAsync(ct);
        
        return true;
    }
}
