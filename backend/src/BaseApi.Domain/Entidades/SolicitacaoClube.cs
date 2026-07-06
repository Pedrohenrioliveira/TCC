using System;

namespace BaseApi.Domain.Entidades;

public enum StatusSolicitacao
{
    Pendente = 1,
    Aceita = 2,
    Recusada = 3
}

public class SolicitacaoClube
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid JogadorId { get; set; }
    public Jogador? Jogador { get; set; }
    
    public Guid ClubeId { get; set; }
    public Clube? Clube { get; set; }
    
    public string Mensagem { get; set; } = string.Empty;
    
    public StatusSolicitacao Status { get; set; } = StatusSolicitacao.Pendente;
    
    public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;
    
    public DateTime? DataResposta { get; set; }
}
