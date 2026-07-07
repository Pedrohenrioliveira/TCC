using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.ClubRequests.Commands.AtualizarStatus;
using BaseApi.Application.ClubRequests.Queries.ListarPorClube;
using BaseApi.Application.Comum.Modelos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.API.Controllers;

[ApiController]
[Route("api/club/club-requests")]
public class ClubRequestsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{clubeId:guid}")]
    [ProducesResponseType(typeof(RespostaApi<IEnumerable<SolicitacaoParaClubeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(Guid clubeId, CancellationToken ct)
    {
        var query = new ListarSolicitacoesClubeQuery { ClubeId = clubeId };
        var resultado = await mediator.Send(query, ct);
        return Ok(resultado);
    }

    [HttpPut("{id:guid}/status")]
    [ProducesResponseType(typeof(RespostaApi<Unit>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AtualizarStatus(Guid id, [FromBody] AtualizarStatusSolicitacaoCommand command, CancellationToken ct)
    {
        if (id != command.Id)
            return BadRequest();

        var resultado = await mediator.Send(command, ct);
        return Ok(resultado);
    }
}
