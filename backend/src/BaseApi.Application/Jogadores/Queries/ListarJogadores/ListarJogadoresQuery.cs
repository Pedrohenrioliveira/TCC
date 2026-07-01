using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Enums;
using MediatR;
using System;

namespace BaseApi.Application.Jogadores.Queries.ListarJogadores;

public record ListarJogadoresQuery(
    int Pagina = 1,
    int TamanhoPagina = 10,
    string? Busca = null
) : IRequest<ResultadoPaginado<JogadorListaDto>>;

public record JogadorListaDto(
    Guid Id,
    string NomeCompleto,
    PosicaoJogador PosicaoPrincipal,
    string NomeClube
);
