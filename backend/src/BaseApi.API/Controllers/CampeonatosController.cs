using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Campeonatos.Commands.AtualizarStatusCampeonato;
using BaseApi.Application.Campeonatos.Commands.CriarCampeonato;
using BaseApi.Application.Campeonatos.Commands.EditarCampeonato;
using BaseApi.Application.Campeonatos.Commands.ExcluirCampeonato;
using BaseApi.Application.Campeonatos.Commands.InscreverClubeCampeonato;
using BaseApi.Application.Campeonatos.Commands.ProcessarInscricaoCampeonato;
using BaseApi.Application.Campeonatos.Commands.GerarCalendario;
using BaseApi.Application.Campeonatos.Commands.AgendarPartidaManual;
using BaseApi.Application.Campeonatos.Commands.AtualizarClassificacaoManual;
using BaseApi.Application.Campeonatos.Queries.ListarCampeonatos;
using BaseApi.Application.Campeonatos.Queries.ObterClassificacao;
using BaseApi.Application.Campeonatos.Queries.ObterInscricoesCampeonato;
using BaseApi.Application.Campeonatos.Queries.ObterMinhasInscricoes;
using BaseApi.Application.Campeonatos.Queries.ObterRodadasCampeonato;
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
        if (!resultado.Ok)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }

    [HttpPut("{id}/status")]
    [ProducesResponseType(typeof(RespostaApi<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AtualizarStatus(Guid id, [FromBody] StatusCampeonato novoStatus, CancellationToken ct)
    {
        var command = new AtualizarStatusCampeonatoCommand { CampeonatoId = id, NovoStatus = novoStatus };
        var resultado = await mediator.Send(command, ct);
        if (!resultado.Ok)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }

    [HttpPost("{id}/clubes")]
    [ProducesResponseType(typeof(RespostaApi<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> InscreverClube(Guid id, [FromBody] InscreverClubeCampeonatoCommand command, CancellationToken ct)
    {
        command.CampeonatoId = id;
        var resultado = await mediator.Send(command, ct);
        if (!resultado.Ok)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }

    [HttpGet("{id}/inscricoes")]
    [ProducesResponseType(typeof(RespostaApi<IEnumerable<InscricaoCampeonatoDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterInscricoes(Guid id, CancellationToken ct)
    {
        var query = new ObterInscricoesCampeonatoQuery { CampeonatoId = id };
        var resultado = await mediator.Send(query, ct);
        return Ok(resultado);
    }

    [HttpGet("minhas-inscricoes/{clubeId}")]
    [ProducesResponseType(typeof(RespostaApi<IEnumerable<InscricaoCampeonatoDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterMinhasInscricoes(Guid clubeId, CancellationToken ct)
    {
        var query = new ObterMinhasInscricoesQuery { ClubeId = clubeId };
        var resultado = await mediator.Send(query, ct);
        return Ok(resultado);
    }

    [HttpPut("inscricoes/{inscricaoId}/processar")]
    [ProducesResponseType(typeof(RespostaApi<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ProcessarInscricao(Guid inscricaoId, [FromBody] ProcessarInscricaoCampeonatoCommand command, CancellationToken ct)
    {
        command.InscricaoId = inscricaoId;
        var resultado = await mediator.Send(command, ct);
        if (!resultado.Ok)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }

    [HttpPost("{id}/gerar-calendario")]
    [ProducesResponseType(typeof(RespostaApi<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GerarCalendario(Guid id, CancellationToken ct)
    {
        var command = new GerarCalendarioCampeonatoCommand { CampeonatoId = id };
        var resultado = await mediator.Send(command, ct);
        if (!resultado.Ok)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }

    [HttpPost("{id}/agendar-partida-manual")]
    [ProducesResponseType(typeof(RespostaApi<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AgendarPartidaManual(Guid id, [FromBody] AgendarPartidaManualCommand command, CancellationToken ct)
    {
        command.CampeonatoId = id;
        var resultado = await mediator.Send(command, ct);
        if (!resultado.Ok)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }

    [HttpPut("{id}/classificacao-manual")]
    [ProducesResponseType(typeof(RespostaApi<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AtualizarClassificacaoManual(Guid id, [FromBody] AtualizarClassificacaoManualCommand command, CancellationToken ct)
    {
        command.CampeonatoId = id;
        var resultado = await mediator.Send(command, ct);
        if (!resultado.Ok)
            return BadRequest(resultado);
            
        return Ok(resultado);
    }

    [HttpGet("{id}/classificacao")]
    [ProducesResponseType(typeof(RespostaApi<IEnumerable<ClassificacaoDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterClassificacao(Guid id, CancellationToken ct)
    {
        var query = new ObterClassificacaoQuery { CampeonatoId = id };
        var resultado = await mediator.Send(query, ct);
        return Ok(resultado);
    }

    [HttpGet("{id}/rodadas")]
    [ProducesResponseType(typeof(RespostaApi<IEnumerable<RodadaDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterRodadas(Guid id, CancellationToken ct)
    {
        var query = new ObterRodadasCampeonatoQuery { CampeonatoId = id };
        var resultado = await mediator.Send(query, ct);
        return Ok(resultado);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RespostaApi<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, [FromBody] EditarCampeonatoCommand command, CancellationToken ct)
    {
        command.Id = id;
        var resultado = await mediator.Send(command, ct);
        return Ok(RespostaApi<bool>.Sucesso(resultado, "Campeonato atualizado com sucesso"));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(RespostaApi<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id, CancellationToken ct)
    {
        var command = new ExcluirCampeonatoCommand { Id = id };
        var resultado = await mediator.Send(command, ct);
        return Ok(RespostaApi<bool>.Sucesso(resultado, "Campeonato excluído com sucesso"));
    }
}
