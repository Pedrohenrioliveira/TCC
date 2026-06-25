using MediatR;
using System;

namespace BaseApi.Application.Clubes.Queries.ObterClubePorId;

public record ObterClubePorIdQuery(Guid Id) : IRequest<ClubeDetalheDto>;

public record ClubeDetalheDto(
    Guid Id,
    string CaminhoEscudo,
    string Nome,
    int AnoFundacao,
    string CidadeEstado,
    string LigaCompeticao,
    string? EstadioPrincipal,
    string BreveHistoria,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
