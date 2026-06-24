using BaseApi.Application.Comum.Modelos;
using MediatR;

namespace BaseApi.Application.Clubes.Queries.ListarClubes;

public record ListarClubesQuery(
    int Pagina = 1,
    int TamanhoPagina = 10,
    string? Busca = null
) : IRequest<ResultadoPaginado<ClubeListaDto>>;

public record ClubeListaDto(
    Guid Id,
    string Nome,
    string CidadeEstado,
    string LigaCompeticao
);
