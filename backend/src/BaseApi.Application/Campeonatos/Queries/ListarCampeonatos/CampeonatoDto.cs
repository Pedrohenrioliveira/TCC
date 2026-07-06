using System;
using BaseApi.Domain.Entidades;

namespace BaseApi.Application.Campeonatos.Queries.ListarCampeonatos;

public class CampeonatoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Local { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public StatusCampeonato Status { get; set; }
    public string CaminhoLogo { get; set; } = string.Empty;
    public int LimiteEquipes { get; set; }
}
