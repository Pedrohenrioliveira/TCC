using System;
using System.Collections.Generic;
using BaseApi.Application.Comum.Modelos;
using MediatR;

namespace BaseApi.Application.ClubRequests.Queries.Listar;

public class ListarSolicitacoesJogadorQuery : IRequest<RespostaApi<IEnumerable<SolicitacaoDto>>>
{
    public Guid JogadorId { get; set; }
}
