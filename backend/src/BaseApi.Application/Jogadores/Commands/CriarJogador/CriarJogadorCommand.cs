using BaseApi.Domain.Enums;
using MediatR;
using System;

namespace BaseApi.Application.Jogadores.Commands.CriarJogador;

public record CriarJogadorCommand : IRequest<CriarJogadorResposta>
{
    public string Email { get; init; } = string.Empty;
    public string Senha { get; init; } = string.Empty;
    public string CaminhoFoto { get; init; } = string.Empty;
    public string NomeCompleto { get; init; } = string.Empty;
    public DateTime DataNascimento { get; init; }
    public PePreferencial PePreferencial { get; init; }
    public int Altura { get; init; }
    public double Peso { get; init; }
    public PosicaoJogador PosicaoPrincipal { get; init; }
    public PosicaoJogador? PosicaoSecundaria { get; init; }
    public string BioHistorico { get; init; } = string.Empty;
    public Guid? ClubeId { get; init; }
}

public record CriarJogadorResposta(
    Guid Id,
    string NomeCompleto,
    PosicaoJogador PosicaoPrincipal
);
