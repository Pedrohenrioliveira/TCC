using System;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Enums;
using MediatR;

namespace BaseApi.Application.Jogadores.Commands.AtualizarJogador;

public class AtualizarDadosFisicosCommand : IRequest<RespostaApi<Unit>>
{
    public Guid JogadorId { get; set; }
    public PePreferencial PePreferencial { get; set; }
    public int Altura { get; set; }
    public double Peso { get; set; }
    public PosicaoJogador PosicaoPrincipal { get; set; }
    public PosicaoJogador? PosicaoSecundaria { get; set; }
}
