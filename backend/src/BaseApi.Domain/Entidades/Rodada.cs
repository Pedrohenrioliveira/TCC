using System;
using System.Collections.Generic;

namespace BaseApi.Domain.Entidades;

public class Rodada
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid CampeonatoId { get; set; }
    public Campeonato Campeonato { get; set; } = null!;
    
    public int Numero { get; set; }
    public string Nome { get; set; } = string.Empty;
    
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

    public ICollection<Partida> Partidas { get; set; } = new List<Partida>();
}
