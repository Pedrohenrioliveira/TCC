using System;

namespace BaseApi.Domain.Entidades;

public class InscricaoCampeonato
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid CampeonatoId { get; set; }
    public Campeonato? Campeonato { get; set; }
    
    public Guid ClubeId { get; set; }
    public Clube? Clube { get; set; }
    
    public StatusInscricao Status { get; set; } = StatusInscricao.Pendente;
    
    public bool AceitouRegulamento { get; set; }
    
    public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;
    
    public DateTime? DataResposta { get; set; }
}
