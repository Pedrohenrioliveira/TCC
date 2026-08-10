using System;

namespace BaseApi.Domain.Entidades;

public enum StatusPartida
{
    Agendada = 1,
    EmAndamento = 2,
    Finalizada = 3,
    Cancelada = 4
}

public class Partida
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid RodadaId { get; set; }
    public Rodada Rodada { get; set; } = null!;
    
    public Guid ClubeMandanteId { get; set; }
    public Clube ClubeMandante { get; set; } = null!;
    
    public Guid ClubeVisitanteId { get; set; }
    public Clube ClubeVisitante { get; set; } = null!;
    
    public int? GolsMandante { get; set; }
    public int? GolsVisitante { get; set; }
    
    public DateTime DataHora { get; set; }
    public string Local { get; set; } = string.Empty;
    
    public StatusPartida Status { get; set; } = StatusPartida.Agendada;
    
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}
