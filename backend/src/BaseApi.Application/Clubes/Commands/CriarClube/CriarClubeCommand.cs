using MediatR;
using System;

namespace BaseApi.Application.Clubes.Commands.CriarClube;

public record CriarClubeCommand : IRequest<CriarClubeResposta>
{
    public string CaminhoEscudo { get; init; } = string.Empty;
    public string Nome { get; init; } = string.Empty;
    public int AnoFundacao { get; init; }
    public string CidadeEstado { get; init; } = string.Empty;
    public string LigaCompeticao { get; init; } = string.Empty;
    public string? EstadioPrincipal { get; init; }
    public string BreveHistoria { get; init; } = string.Empty;
}

public record CriarClubeResposta(
    Guid Id,
    string Nome,
    string CidadeEstado
);
