using System;
using BaseApi.Domain.Entidades;

namespace BaseApi.Application.ClubRequests.Queries.Listar;

public class SolicitacaoDto
{
    public Guid Id { get; set; }
    public Guid ClubeId { get; set; }
    public string NomeClube { get; set; } = string.Empty;
    public string EscudoClube { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
    public StatusSolicitacao Status { get; set; }
    public DateTime DataSolicitacao { get; set; }
}
