using BaseApi.Domain.Enums;
using MediatR;
using System;

namespace BaseApi.Application.Jogadores.Commands.CriarJogador;

public record CriarJogadorCommand(
    string Email,
    string Senha,
    string CaminhoFoto,
    string NomeCompleto,
    DateTime DataNascimento,
    PePreferencial PePreferencial,
    int Altura,
    double Peso,
    PosicaoJogador PosicaoPrincipal,
    PosicaoJogador? PosicaoSecundaria,
    string BioHistorico,
    Guid? ClubeId
) : IRequest<CriarJogadorResposta>;

public record CriarJogadorResposta(
    Guid Id,
    string NomeCompleto,
    PosicaoJogador PosicaoPrincipal
);
