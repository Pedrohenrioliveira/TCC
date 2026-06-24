using System;

namespace BaseApi.Domain.Entidades;

/// <summary>
/// Entidade que representa um clube de futebol cadastrado no sistema.
/// </summary>
public class Clube
{
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Caminho ou URL do arquivo do escudo do clube.
    /// </summary>
    public string CaminhoEscudo { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public int AnoFundacao { get; set; }

    public string CidadeEstado { get; set; } = string.Empty;

    public string LigaCompeticao { get; set; } = string.Empty;

    public string? EstadioPrincipal { get; set; }

    public string BreveHistoria { get; set; } = string.Empty;

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}
