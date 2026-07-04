using BaseApi.Application.Comum.Modelos;
using BaseApi.Application.Ligas.Queries.ObterTabelaClassificacao;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
[Produces("application/json")]
public class LigasController(IMediator mediator) : ControllerBase
{
    // =========================================================
    // GET /api/ligas/{id}/standings
    // =========================================================
    /// <summary>
    /// Obtém a tabela de classificação de uma liga/campeonato.
    /// </summary>
    [HttpGet("{id:guid}/standings")]
    [ProducesResponseType(typeof(RespostaApi<TabelaClassificacaoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTabelaClassificacao(Guid id, CancellationToken ct)
    {
        var resultado = await mediator.Send(new ObterTabelaClassificacaoQuery(id), ct);
        return Ok(RespostaApi<TabelaClassificacaoDto>.Sucesso(resultado));
    }
}
