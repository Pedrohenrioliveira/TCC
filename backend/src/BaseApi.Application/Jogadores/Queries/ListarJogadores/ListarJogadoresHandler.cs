using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Interfaces.Repositorios;
using Mapster;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Jogadores.Queries.ListarJogadores;

public class ListarJogadoresHandler(IJogadorRepositorio repositorio) 
    : IRequestHandler<ListarJogadoresQuery, ResultadoPaginado<JogadorListaDto>>
{
    public async Task<ResultadoPaginado<JogadorListaDto>> Handle(ListarJogadoresQuery query, CancellationToken ct)
    {
        var (itens, total) = await repositorio.ListarAsync(query.Pagina, query.TamanhoPagina, query.Busca, ct);

        return new ResultadoPaginado<JogadorListaDto>
        {
            Itens = itens.Adapt<IEnumerable<JogadorListaDto>>(),
            Total = total,
            Pagina = query.Pagina,
            TamanhoPagina = query.TamanhoPagina
        };
    }
}
