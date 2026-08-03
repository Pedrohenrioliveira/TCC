using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Application.Partidas.Commands.AgendarPartida;
using BaseApi.Application.Partidas.Commands.AtualizarPlacarPartida;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartidasController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RespostaApi<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Agendar([FromBody] AgendarPartidaCommand command, CancellationToken ct)
    {
        var resultado = await mediator.Send(command, ct);
        if (!resultado.DeuCerto)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }

    [HttpPut("{id}/placar")]
    [ProducesResponseType(typeof(RespostaApi<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AtualizarPlacar(Guid id, [FromBody] AtualizarPlacarPartidaCommand command, CancellationToken ct)
    {
        command.PartidaId = id;
        var resultado = await mediator.Send(command, ct);
        if (!resultado.DeuCerto)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }
}
