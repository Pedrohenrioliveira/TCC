using BaseApi.Application.Jogadores.Commands.CriarJogador;
using BaseApi.Application.Jogadores.Queries.ListarJogadores;
using BaseApi.Application.Jogadores.Queries.ObterJogadorPorId;
using BaseApi.Domain.Entidades;
using Mapster;

namespace BaseApi.Application.Comum.Mapeamentos;

/// <summary>
/// Configuração de mapeamentos Mapster para a entidade Jogador.
/// </summary>
public class JogadorMapeamento : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Jogador -> JogadorDetalheDto
        config.NewConfig<Jogador, JogadorDetalheDto>()
            .Map(dest => dest.NomeClube, src => src.Clube != null ? src.Clube.Nome : string.Empty);

        // Jogador -> JogadorListaDto
        config.NewConfig<Jogador, JogadorListaDto>()
            .Map(dest => dest.NomeClube, src => src.Clube != null ? src.Clube.Nome : string.Empty);

        // CriarJogadorCommand -> Jogador
        config.NewConfig<CriarJogadorCommand, Jogador>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Clube!)
            .Ignore(dest => dest.CriadoEm)
            .Ignore(dest => dest.AtualizadoEm);
    }
}
