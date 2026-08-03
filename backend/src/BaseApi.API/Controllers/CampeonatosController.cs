using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Campeonatos.Commands.AtualizarStatusCampeonato;
using BaseApi.Application.Campeonatos.Commands.CriarCampeonato;
using BaseApi.Application.Campeonatos.Commands.InscreverClubeCampeonato;
using BaseApi.Application.Campeonatos.Queries.ListarCampeonatos;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
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

    [HttpPost]
    [ProducesResponseType(typeof(RespostaApi<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Criar([FromBody] CriarCampeonatoCommand command, CancellationToken ct)
    {
        var resultado = await mediator.Send(command, ct);
        if (!resultado.DeuCerto)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }

    [HttpPut("{id}/status")]
    [ProducesResponseType(typeof(RespostaApi<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AtualizarStatus(Guid id, [FromBody] StatusCampeonato novoStatus, CancellationToken ct)
    {
        var command = new AtualizarStatusCampeonatoCommand { CampeonatoId = id, NovoStatus = novoStatus };
        var resultado = await mediator.Send(command, ct);
        if (!resultado.DeuCerto)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }

    [HttpPost("{id}/clubes")]
    [ProducesResponseType(typeof(RespostaApi<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> InscreverClube(Guid id, [FromBody] Guid clubeId, CancellationToken ct)
    {
        var command = new InscreverClubeCampeonatoCommand { CampeonatoId = id, ClubeId = clubeId };
        var resultado = await mediator.Send(command, ct);
        if (!resultado.DeuCerto)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }
}
