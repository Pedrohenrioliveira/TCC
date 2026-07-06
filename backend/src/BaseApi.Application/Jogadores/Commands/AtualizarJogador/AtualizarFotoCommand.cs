using System;
using BaseApi.Application.Comum.Modelos;
using MediatR;

namespace BaseApi.Application.Jogadores.Commands.AtualizarJogador;

public class AtualizarFotoCommand : IRequest<RespostaApi<Unit>>
{
    public Guid JogadorId { get; set; }
    public string CaminhoFoto { get; set; } = string.Empty;
}
