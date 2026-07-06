using System;
using System.Collections.Generic;
using BaseApi.Application.Comum.Modelos;
using MediatR;

namespace BaseApi.Application.Campeonatos.Queries.ListarCampeonatos;

public class ListarCampeonatosQuery : IRequest<RespostaApi<IEnumerable<CampeonatoDto>>>
{
    public string? Status { get; set; }
}
