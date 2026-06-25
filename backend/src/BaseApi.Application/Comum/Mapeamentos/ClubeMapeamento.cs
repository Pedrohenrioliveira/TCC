using BaseApi.Application.Clubes.Commands.CriarClube;
using BaseApi.Application.Clubes.Queries.ListarClubes;
using BaseApi.Application.Clubes.Queries.ObterClubePorId;
using BaseApi.Domain.Entidades;
using Mapster;

namespace BaseApi.Application.Comum.Mapeamentos;

/// <summary>
/// Configuração de mapeamentos Mapster para a entidade Clube.
/// </summary>
public class ClubeMapeamento : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // CriarClubeCommand -> Clube
        config.NewConfig<CriarClubeCommand, Clube>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.CriadoEm)
            .Ignore(dest => dest.AtualizadoEm);
    }
}
