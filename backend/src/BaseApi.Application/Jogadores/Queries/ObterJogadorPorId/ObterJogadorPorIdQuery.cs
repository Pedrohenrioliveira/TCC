using BaseApi.Domain.Enums;
using MediatR;
using System;

namespace BaseApi.Application.Jogadores.Queries.ObterJogadorPorId;

public record ObterJogadorPorIdQuery(Guid Id) : IRequest<JogadorDetalheDto>;

public record JogadorDetalheDto(
    Guid Id,
    string CaminhoFoto,
    string NomeCompleto,
    DateTime DataNascimento,
    PePreferencial PePreferencial,
    int Altura,
    double Peso,
    PosicaoJogador PosicaoPrincipal,
    PosicaoJogador? PosicaoSecundaria,
    string BioHistorico,
    Guid? ClubeId,
    string NomeClube,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
