using System;

namespace BaseApi.Domain.Entidades;

public enum StatusCampeonato
{
    Aberto = 1,
    EmAndamento = 2,
    Finalizado = 3
}

public class Campeonato
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Nome { get; set; } = string.Empty;
    
    public string Local { get; set; } = string.Empty;
    
    public DateTime DataInicio { get; set; }
    
    public DateTime DataFim { get; set; }
    
    public StatusCampeonato Status { get; set; } = StatusCampeonato.Aberto;
    
    public string CaminhoLogo { get; set; } = string.Empty;
    
    public decimal TaxaInscricao { get; set; }
    
    public string ChavePix { get; set; } = string.Empty;
    
    public string DiasDosJogos { get; set; } = string.Empty;
    
    public string CaminhoImagemCampo { get; set; } = string.Empty;
    
    public string Descricao { get; set; } = string.Empty;
    
    public int LimiteEquipes { get; set; }
    
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}
