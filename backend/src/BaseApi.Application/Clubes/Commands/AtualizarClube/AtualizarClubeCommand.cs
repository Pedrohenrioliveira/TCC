using MediatR;
using System;

namespace BaseApi.Application.Clubes.Commands.AtualizarClube;

public record AtualizarClubeCommand(
    Guid Id,
    string CaminhoEscudo,
    string Nome,
    int AnoFundacao,
    string CidadeEstado,
    string LigaCompeticao,
    string? EstadioPrincipal,
    string BreveHistoria
) : IRequest;
