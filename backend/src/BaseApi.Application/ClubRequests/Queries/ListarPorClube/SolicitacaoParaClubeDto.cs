using System;
using BaseApi.Domain.Entidades;

namespace BaseApi.Application.ClubRequests.Queries.ListarPorClube;

public class SolicitacaoParaClubeDto
{
    public Guid Id { get; set; }
    public Guid JogadorId { get; set; }
    public string NomeJogador { get; set; } = string.Empty;
    public string CaminhoFotoJogador { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
    public StatusSolicitacao Status { get; set; }
    public DateTime DataSolicitacao { get; set; }
}
