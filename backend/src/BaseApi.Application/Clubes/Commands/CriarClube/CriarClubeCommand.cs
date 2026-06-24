using MediatR;
using System;

namespace BaseApi.Application.Clubes.Commands.CriarClube;

public record CriarClubeCommand(
    string CaminhoEscudo,
    string Nome,
    int AnoFundacao,
    string CidadeEstado,
    string LigaCompeticao,
    string? EstadioPrincipal,
    string BreveHistoria
) : IRequest<CriarClubeResposta>;

public record CriarClubeResposta(
    Guid Id,
    string Nome,
    string CidadeEstado
);
