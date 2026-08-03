using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Application.Rodadas.Commands.CriarRodada;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RodadasController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RespostaApi<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Criar([FromBody] CriarRodadaCommand command, CancellationToken ct)
    {
        var resultado = await mediator.Send(command, ct);
        if (!resultado.DeuCerto)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }
}
