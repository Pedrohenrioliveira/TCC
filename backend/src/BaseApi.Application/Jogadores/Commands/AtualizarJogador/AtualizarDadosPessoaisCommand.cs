using System;
using BaseApi.Application.Comum.Modelos;
using MediatR;

namespace BaseApi.Application.Jogadores.Commands.AtualizarJogador;

public class AtualizarDadosPessoaisCommand : IRequest<RespostaApi<Unit>>
{
    public Guid JogadorId { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string BioHistorico { get; set; } = string.Empty;
}
