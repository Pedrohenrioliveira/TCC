using System;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;

namespace BaseApi.Application.ClubRequests.Commands.AtualizarStatus;

public class AtualizarStatusSolicitacaoCommand : IRequest<RespostaApi<Unit>>
{
    public Guid Id { get; set; }
    public StatusSolicitacao NovoStatus { get; set; }
}
