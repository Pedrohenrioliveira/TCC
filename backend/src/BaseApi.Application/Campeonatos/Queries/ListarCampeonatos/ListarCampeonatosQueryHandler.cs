using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Interfaces.Repositorios;
using MediatR;

namespace BaseApi.Application.Campeonatos.Queries.ListarCampeonatos;

public class ListarCampeonatosQueryHandler(ICampeonatoRepositorio repositorio) 
    : IRequestHandler<ListarCampeonatosQuery, RespostaApi<IEnumerable<CampeonatoDto>>>
{
    public async Task<RespostaApi<IEnumerable<CampeonatoDto>>> Handle(ListarCampeonatosQuery request, CancellationToken ct)
    {
        var campeonatos = await repositorio.ListarAsync(request.Status, ct);

        var dto = campeonatos.Select(c => new CampeonatoDto
        {
            Id = c.Id,
            Nome = c.Nome,
            Local = c.Local,
            DataInicio = c.DataInicio,
            DataFim = c.DataFim,
            Status = c.Status,
            CaminhoLogo = c.CaminhoLogo,
            LimiteEquipes = c.LimiteEquipes
        });

        return RespostaApi<IEnumerable<CampeonatoDto>>.Sucesso(dto);
    }
}
