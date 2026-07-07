using System;
using System.Collections.Generic;
using BaseApi.Application.Comum.Modelos;
using MediatR;

namespace BaseApi.Application.ClubRequests.Queries.ListarPorClube;

public class ListarSolicitacoesClubeQuery : IRequest<RespostaApi<IEnumerable<SolicitacaoParaClubeDto>>>
{
    public Guid ClubeId { get; set; }
}
