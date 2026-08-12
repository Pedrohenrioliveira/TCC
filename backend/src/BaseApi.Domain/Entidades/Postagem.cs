using System;

namespace BaseApi.Domain.Entidades;

/// <summary>
/// Representa uma foto postada no feed (Galeria) de um jogador ou clube.
/// </summary>
public class Postagem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// URL ou caminho relativo da foto salva no servidor.
    /// </summary>
    public string CaminhoFoto { get; set; } = string.Empty;

    /// <summary>
    /// Descrição opcional (legenda) da foto.
    /// </summary>
    public string Descricao { get; set; } = string.Empty;

    public DateTime DataPostagem { get; set; } = DateTime.UtcNow;

    // Vínculos (Um deles será preenchido)
    public Guid? JogadorId { get; set; }
    public Jogador? Jogador { get; set; }

    public Guid? ClubeId { get; set; }
    public Clube? Clube { get; set; }
}
