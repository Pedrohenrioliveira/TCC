using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Campeonatos.Queries.ListarCampeonatos;
using BaseApi.Application.Comum.Modelos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampeonatosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(RespostaApi<IEnumerable<CampeonatoDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar([FromQuery] string? status, CancellationToken ct)
    {
        var query = new ListarCampeonatosQuery { Status = status };
        var resultado = await mediator.Send(query, ct);
        return Ok(resultado);
    }
}
