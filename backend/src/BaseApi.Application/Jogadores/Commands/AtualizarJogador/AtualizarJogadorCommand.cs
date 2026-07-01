using BaseApi.Domain.Enums;
using MediatR;
using System;

namespace BaseApi.Application.Jogadores.Commands.AtualizarJogador;

public record AtualizarJogadorCommand(
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
    Guid? ClubeId
) : IRequest;
