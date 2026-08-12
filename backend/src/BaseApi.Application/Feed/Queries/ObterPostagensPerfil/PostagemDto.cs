using System;

namespace BaseApi.Application.Feed.Queries.ObterPostagensPerfil;

public class PostagemDto
{
    public Guid Id { get; set; }
    public string CaminhoFoto { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataPostagem { get; set; }
}
