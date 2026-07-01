using BaseApi.Application.Comum.Modelos;
using BaseApi.Application.Jogadores.Commands.AtualizarJogador;
using BaseApi.Application.Jogadores.Commands.CriarJogador;
using BaseApi.Application.Jogadores.Commands.ExcluirJogador;
using BaseApi.Application.Jogadores.Queries.ListarJogadores;
using BaseApi.Application.Jogadores.Queries.ObterJogadorPorId;
using BaseApi.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.API.Controllers;

/// <summary>
/// Endpoints para cadastro e gerenciamento de jogadores de futebol (talentos).
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous] // Permitir acesso sem autenticação JWT conforme solicitado
[Produces("application/json")]
public class JogadoresController(IMediator mediator) : ControllerBase
{
    // =========================================================
    // GET /api/jogadores?pagina=1&tamanhoPagina=10&busca=gabriel
    // =========================================================
    /// <summary>
    /// Lista jogadores cadastrados com paginação e busca textual.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(RespostaApi<ResultadoPaginado<JogadorListaDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 10,
        [FromQuery] string? busca = null,
        CancellationToken ct = default)
    {
        var resultado = await mediator.Send(new ListarJogadoresQuery(pagina, tamanhoPagina, busca), ct);
        return Ok(RespostaApi<ResultadoPaginado<JogadorListaDto>>.Sucesso(resultado));
    }

    // =========================================================
    // GET /api/jogadores/{id}
    // =========================================================
    /// <summary>
    /// Obtém detalhes de um jogador pelo ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RespostaApi<JogadorDetalheDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken ct)
    {
        var resultado = await mediator.Send(new ObterJogadorPorIdQuery(id), ct);
        return Ok(RespostaApi<JogadorDetalheDto>.Sucesso(resultado));
    }

    // =========================================================
    // POST /api/jogadores
    // =========================================================
    /// <summary>
    /// Cadastra um novo jogador no sistema.
    /// </summary>
    /// <remarks>
    /// Exemplo de corpo da requisição (Body):
    /// 
    ///     {
    ///       "caminhoFoto": "https://link-da-foto.com/foto.png",
    ///       "nomeCompleto": "Gabriel Barbosa",
    ///       "dataNascimento": "1996-08-30T00:00:00",
    ///       "pePreferencial": 2,
    ///       "altura": 178,
    ///       "peso": 73,
    ///       "posicaoPrincipal": 8,
    ///       "posicaoSecundaria": 7,
    ///       "bioHistorico": "Gabriel Barbosa Almeida, mais conhecido como Gabigol, é um futebolista brasileiro que atua como atacante.",
    ///       "clubeId": null
    ///     }
    /// 
    /// Valores para Pé Preferencial:
    ///   1 = Esquerdo, 2 = Direito, 3 = Ambos
    /// 
    /// Valores para Posição:
    ///   1 = Goleiro, 2 = LateralDireito, 3 = Zagueiro, 4 = LateralEsquerdo,
    ///   5 = Volante, 6 = MeioCampo, 7 = Ponta, 8 = Centroavante
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(RespostaApi<CriarJogadorResposta>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CriarJogadorCommand command, CancellationToken ct)
    {
        var resultado = await mediator.Send(command, ct);
        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = resultado.Id },
            RespostaApi<CriarJogadorResposta>.Sucesso(resultado, "Jogador cadastrado com sucesso!"));
    }

    // =========================================================
    // PUT /api/jogadores/{id}
    // =========================================================
    /// <summary>
    /// Atualiza os dados de um jogador existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarJogadorRequest request, CancellationToken ct)
    {
        var command = new AtualizarJogadorCommand(
            id,
            request.CaminhoFoto,
            request.NomeCompleto,
            request.DataNascimento,
            request.PePreferencial,
            request.Altura,
            request.Peso,
            request.PosicaoPrincipal,
            request.PosicaoSecundaria,
            request.BioHistorico,
            request.ClubeId
        );
        await mediator.Send(command, ct);
        return Ok(RespostaApi.Sucesso("Jogador atualizado com sucesso!"));
    }

    // =========================================================
    // DELETE /api/jogadores/{id}
    // =========================================================
    /// <summary>
    /// Remove um jogador do sistema.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaApi), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(Guid id, CancellationToken ct)
    {
        await mediator.Send(new ExcluirJogadorCommand(id), ct);
        return Ok(RespostaApi.Sucesso("Jogador excluído com sucesso!"));
    }
}

/// <summary>
/// Modelo de requisição para atualizar os dados de um jogador.
/// </summary>
public record AtualizarJogadorRequest(
    string CaminhoFoto,
    string NomeCompleto,
    DateTime DataNascimento,
    PePreferencial PePreferencial,
    int Altura,
    double Peso,
    PosicaoJogador PosicaoPrincipal,
    PosicaoJogador? PosicaoSecundaria,
    string BioHistorico,
    Guid? ClubeId
);
