using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Interfaces.Repositorios;
using Mapster;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Clubes.Queries.ListarClubes;

public class ListarClubesHandler(IClubeRepositorio repositorio) 
    : IRequestHandler<ListarClubesQuery, ResultadoPaginado<ClubeListaDto>>
{
    public async Task<ResultadoPaginado<ClubeListaDto>> Handle(ListarClubesQuery query, CancellationToken ct)
    {
        var (itens, total) = await repositorio.ListarAsync(query.Pagina, query.TamanhoPagina, query.Busca, ct);

        return new ResultadoPaginado<ClubeListaDto>
        {
            Itens = itens.Adapt<IEnumerable<ClubeListaDto>>(),
            Total = total,
            Pagina = query.Pagina,
            TamanhoPagina = query.TamanhoPagina
        };
    }
}
