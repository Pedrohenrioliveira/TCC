using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.ClubRequests.Commands.AtualizarStatus;
using BaseApi.Application.ClubRequests.Commands.Criar;
using BaseApi.Application.ClubRequests.Queries.Listar;
using BaseApi.Application.Comum.Modelos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.API.Controllers;

[ApiController]
[Route("api/player/club-requests")]
public class PlayerClubRequestsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{jogadorId:guid}")]
    [ProducesResponseType(typeof(RespostaApi<IEnumerable<SolicitacaoDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(Guid jogadorId, CancellationToken ct)
    {
        var query = new ListarSolicitacoesJogadorQuery { JogadorId = jogadorId };
        var resultado = await mediator.Send(query, ct);
        return Ok(resultado);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RespostaApi<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Criar([FromBody] CriarSolicitacaoCommand command, CancellationToken ct)
    {
        var resultado = await mediator.Send(command, ct);
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
