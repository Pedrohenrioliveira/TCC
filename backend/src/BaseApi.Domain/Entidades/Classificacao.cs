using System;

namespace BaseApi.Domain.Entidades;

public class Classificacao
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid CampeonatoId { get; set; }
    public Campeonato Campeonato { get; set; } = null!;
    
    public Guid ClubeId { get; set; }
    public Clube Clube { get; set; } = null!;
    
    public int Pontos { get; set; }
    public int PartidasJogadas { get; set; }
    public int Vitorias { get; set; }
    public int Empates { get; set; }
    public int Derrotas { get; set; }
    
    public int GolsPro { get; set; }
    public int GolsContra { get; set; }
    
    public int SaldoGols => GolsPro - GolsContra;
    
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}
