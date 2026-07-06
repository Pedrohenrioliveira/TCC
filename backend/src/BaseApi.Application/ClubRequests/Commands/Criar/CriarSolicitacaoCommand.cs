using System;
using BaseApi.Application.Comum.Modelos;
using MediatR;

namespace BaseApi.Application.ClubRequests.Commands.Criar;

public class CriarSolicitacaoCommand : IRequest<RespostaApi<Guid>>
{
    public Guid JogadorId { get; set; }
    public Guid ClubeId { get; set; }
    public string Mensagem { get; set; } = string.Empty;
}
