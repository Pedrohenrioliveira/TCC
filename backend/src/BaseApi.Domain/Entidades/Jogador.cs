using BaseApi.Domain.Enums;
using System;

namespace BaseApi.Domain.Entidades;

/// <summary>
/// Entidade que representa um jogador de futebol (talento) cadastrado no sistema.
/// </summary>
public class Jogador
{
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Caminho ou URL da foto do atleta.
    /// </summary>
    public string CaminhoFoto { get; set; } = string.Empty;

    public string NomeCompleto { get; set; } = string.Empty;

    public DateTime DataNascimento { get; set; }

    public PePreferencial PePreferencial { get; set; }

    /// <summary>
    /// Altura do jogador em centímetros (ex: 185).
    /// </summary>
    public int Altura { get; set; }

    /// <summary>
    /// Peso do jogador em quilogramas (ex: 78.5).
    /// </summary>
    public double Peso { get; set; }

    public PosicaoJogador PosicaoPrincipal { get; set; }

    public PosicaoJogador? PosicaoSecundaria { get; set; }

    public string BioHistorico { get; set; } = string.Empty;

    /// <summary>
    /// Associação opcional com um clube de futebol.
    /// </summary>
    public Guid? ClubeId { get; set; }

    public Clube? Clube { get; set; }

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}
