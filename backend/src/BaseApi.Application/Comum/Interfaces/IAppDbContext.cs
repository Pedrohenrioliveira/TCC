using System.Threading;
using System.Threading.Tasks;
using BaseApi.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Comum.Interfaces;

public interface IAppDbContext
{
    DbSet<Usuario> Usuarios { get; }
    DbSet<Perfil> Perfis { get; }
    DbSet<Clube> Clubes { get; }
    DbSet<Jogador> Jogadores { get; }
    
    DbSet<Campeonato> Campeonatos { get; }
    DbSet<Rodada> Rodadas { get; }
    DbSet<Partida> Partidas { get; }
    DbSet<Classificacao> Classificacoes { get; }
    DbSet<InscricaoCampeonato> InscricoesCampeonatos { get; }
    
    DbSet<Postagem> Postagens { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
