using BaseApi.Application.Comum.Modelos;
using BaseApi.Application.Clubes.Commands.AtualizarClube;
using BaseApi.Application.Clubes.Commands.CriarClube;
using BaseApi.Application.Clubes.Commands.ExcluirClube;
using BaseApi.Application.Clubes.Queries.ListarClubes;
using BaseApi.Application.Clubes.Queries.ObterClubePorId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.API.Controllers;

/// <summary>
/// Endpoints para cadastro e gerenciamento de clubes de futebol.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous] // Permitir acesso sem autenticação JWT conforme solicitado
[Produces("application/json")]
public class ClubesController(IMediator mediator) : ControllerBase
{
    // =========================================================
    // GET /api/clubes?pagina=1&tamanhoPagina=10&busca=santos
    // =========================================================
    /// <summary>
    /// Lista clubes cadastrados com paginação e busca textual.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(RespostaApi<ResultadoPaginado<ClubeListaDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 10,
        [FromQuery] string? busca = null,
        CancellationToken ct = default)
    {
        var resultado = await mediator.Send(new ListarClubesQuery(pagina, tamanhoPagina, busca), ct);
        return Ok(RespostaApi<ResultadoPaginado<ClubeListaDto>>.Sucesso(resultado));
    }

    // =========================================================
    // GET /api/clubes/{id}
    // =========================================================
    /// <summary>
    /// Obtém detalhes de um clube pelo ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RespostaApi<ClubeDetalheDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken ct)
    {
        var resultado = await mediator.Send(new ObterClubePorIdQuery(id), ct);
        return Ok(RespostaApi<ClubeDetalheDto>.Sucesso(resultado));
    }

    // =========================================================
    // POST /api/clubes
    // =========================================================
    /// <summary>
    /// Cadastra um novo clube no sistema.
    /// </summary>
    /// <remarks>
    /// Exemplo de corpo da requisição (Body):
    /// 
    ///     {
    ///       "caminhoEscudo": "https://link-do-escudo.com/escudo.png",
    ///       "nome": "Santos FC",
    ///       "anoFundacao": 1912,
    ///       "cidadeEstado": "Santos/SP",
    ///       "ligaCompeticao": "Série A, Paulistão",
    ///       "estadioPrincipal": "Vila Belmiro",
    ///       "breveHistoria": "Santos Futebol Clube é um clube de futebol fundado em 1912..."
    ///     }
    /// 
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(RespostaApi<CriarClubeResposta>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CriarClubeCommand command, CancellationToken ct)
    {
        var resultado = await mediator.Send(command, ct);
        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = resultado.Id },
            RespostaApi<CriarClubeResposta>.Sucesso(resultado, "Clube cadastrado com sucesso!"));
    }

    // =========================================================
    // PUT /api/clubes/{id}
    // =========================================================
    /// <summary>
    /// Atualiza os dados de um clube existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarClubeRequest request, CancellationToken ct)
    {
        var command = new AtualizarClubeCommand(
            id,
            request.CaminhoEscudo,
            request.Nome,
            request.AnoFundacao,
            request.CidadeEstado,
            request.LigaCompeticao,
            request.EstadioPrincipal,
            request.BreveHistoria
        );
        await mediator.Send(command, ct);
        return Ok(RespostaApi.Sucesso("Clube atualizado com sucesso!"));
    }

    // =========================================================
    // DELETE /api/clubes/{id}
    // =========================================================
    /// <summary>
    /// Remove um clube do sistema.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(Guid id, CancellationToken ct)
    {
        await mediator.Send(new ExcluirClubeCommand(id), ct);
        return Ok(RespostaApi.Sucesso("Clube excluído com sucesso!"));
    }
}

/// <summary>
/// Modelo de requisição para atualizar os dados de um clube.
/// </summary>
public record AtualizarClubeRequest(
    string CaminhoEscudo,
    string Nome,
    int AnoFundacao,
    string CidadeEstado,
    string LigaCompeticao,
    string? EstadioPrincipal,
    string BreveHistoria
);
