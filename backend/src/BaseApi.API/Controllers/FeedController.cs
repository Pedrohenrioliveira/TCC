using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Application.Feed.Commands.AdicionarPostagem;
using BaseApi.Application.Feed.Queries.ObterPostagensPerfil;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.API.Controllers;

public class AdicionarPostagemDto
{
    public Guid PerfilId { get; set; }
    public IFormFile? Foto { get; set; }
    public string Descricao { get; set; } = string.Empty;
}

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous] // Simplificação para TCC
[Produces("application/json")]
public class FeedController(IMediator mediator) : ControllerBase
{
    // =========================================================
    // POST /api/feed
    // =========================================================
    /// <summary>
    /// Adiciona uma nova postagem ao feed de um perfil (Jogador ou Clube).
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(RespostaApi<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AdicionarPostagem([FromForm] AdicionarPostagemDto dto, CancellationToken ct)
    {
        if (dto.Foto == null || dto.Foto.Length == 0)
        {
            return BadRequest(RespostaApi<Guid>.Falha("Nenhuma imagem enviada."));
        }

        using var ms = new System.IO.MemoryStream();
        await dto.Foto.CopyToAsync(ms, ct);
        var base64String = Convert.ToBase64String(ms.ToArray());
        var fotoUrl = $"data:{dto.Foto.ContentType};base64,{base64String}";

        var command = new AdicionarPostagemCommand
        {
            PerfilId = dto.PerfilId,
            CaminhoFoto = fotoUrl,
            Descricao = dto.Descricao
        };

        var resultado = await mediator.Send(command, ct);
        if (resultado.Ok) return Ok(resultado);
        return BadRequest(resultado);
    }

    // =========================================================
    // GET /api/feed/{perfilId}
    // =========================================================
    /// <summary>
    /// Lista as postagens (fotos) do perfil solicitado, da mais nova para a mais velha.
    /// </summary>
    [HttpGet("{perfilId:guid}")]
    [ProducesResponseType(typeof(RespostaApi<List<PostagemDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterPostagens(Guid perfilId, CancellationToken ct)
    {
        var postagens = await mediator.Send(new ObterPostagensPerfilQuery(perfilId), ct);
        return Ok(RespostaApi<List<PostagemDto>>.Sucesso(postagens));
    }
}
