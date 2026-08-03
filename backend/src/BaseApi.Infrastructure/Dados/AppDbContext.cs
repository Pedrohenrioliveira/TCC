using BaseApi.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Infrastructure.Dados;

/// <summary>
/// Contexto principal do Entity Framework Core.
/// Gerencia todas as entidades e a conexão com o banco MySQL.
///
/// As migrations são criadas pelo CLI do EF e aplicadas automaticamente no startup.
/// Para criar uma nova migration após alterar entidades:
///   dotnet ef migrations add NomeDaMigration --project src/BaseApi.Infrastructure --startup-project src/BaseApi.API
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Perfil> Perfis => Set<Perfil>();
    public DbSet<Clube> Clubes => Set<Clube>();
    public DbSet<Jogador> Jogadores => Set<Jogador>();
    public DbSet<Campeonato> Campeonatos => Set<Campeonato>();
    public DbSet<SolicitacaoClube> SolicitacoesClubes => Set<SolicitacaoClube>();
    public DbSet<Rodada> Rodadas => Set<Rodada>();
    public DbSet<Partida> Partidas => Set<Partida>();
    public DbSet<Classificacao> Classificacoes => Set<Classificacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica todas as configurações do assembly automaticamente (Fluent API)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // =============================================
        // SEED — dados iniciais criados automaticamente
        // =============================================
        SeedPerfis(modelBuilder);
        SeedUsuarioAdmin(modelBuilder);
        SeedCampeonatos(modelBuilder);
        SeedJogadores(modelBuilder);
        SeedClubes(modelBuilder);
        SeedSolicitacoesClubes(modelBuilder);
    }

    private static void SeedPerfis(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Perfil>().HasData(
            new Perfil { Id = 1, Nome = "Admin",    Descricao = "Acesso total ao sistema" },
            new Perfil { Id = 2, Nome = "Gerente",  Descricao = "Acesso intermediário ao sistema" },
            new Perfil { Id = 3, Nome = "Usuário",  Descricao = "Acesso básico ao sistema" },
            new Perfil { Id = 4, Nome = "Jogador",  Descricao = "Perfil de acesso para Jogadores" },
            new Perfil { Id = 5, Nome = "Clube",    Descricao = "Perfil de acesso para Clubes" }
        );
    }

    private static void SeedUsuarioAdmin(ModelBuilder modelBuilder)
    {
        // Usuário padrão: admin@baseapi.com / Admin@123
        // Hash gerado com BCrypt (work factor 12)
        modelBuilder.Entity<Usuario>().HasData(new Usuario
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            NomeCompleto = "Administrador do Sistema",
            NomeUsuario = "admin",
            Email = "admin@baseapi.com",
            SenhaHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            PerfilId = 1,
            Ativo = true,
            CriadoEm = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            AtualizadoEm = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
    }

    private static void SeedCampeonatos(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campeonato>().HasData(
            new Campeonato
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Nome = "Copa Regional Norte 2026",
                Local = "Estádio Municipal",
                DataInicio = new DateTime(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc),
                DataFim = new DateTime(2026, 12, 1, 0, 0, 0, DateTimeKind.Utc),
                Status = StatusCampeonato.Aberto,
                CaminhoLogo = "assets/campeonato1.jpg",
                LimiteEquipes = 16
            },
            new Campeonato
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Nome = "Liga dos Campeões Amadora",
                Local = "Vários Estádios",
                DataInicio = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                DataFim = new DateTime(2026, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                Status = StatusCampeonato.EmAndamento,
                CaminhoLogo = "assets/campeonato2.jpg",
                LimiteEquipes = 32
            },
            new Campeonato
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Nome = "Torneio de Inverno 2025",
                Local = "Arena Sul",
                DataInicio = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                DataFim = new DateTime(2025, 8, 1, 0, 0, 0, DateTimeKind.Utc),
                Status = StatusCampeonato.Finalizado,
                CaminhoLogo = "assets/campeonato3.jpg",
                LimiteEquipes = 8
            }
        );
    }

    private static void SeedJogadores(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Jogador>().HasData(
            new Jogador
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UsuarioId = Guid.Parse("00000000-0000-0000-0000-000000000001"), // Vinculado ao admin
                NomeCompleto = "Pedro Oliveira (Teste)",
                DataNascimento = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                PePreferencial = BaseApi.Domain.Enums.PePreferencial.Ambos,
                Altura = 180,
                Peso = 75,
                PosicaoPrincipal = BaseApi.Domain.Enums.PosicaoJogador.MeioCampo,
                BioHistorico = "Jogador de teste do sistema.",
                CaminhoFoto = "https://robohash.org/pedro?set=set5"
            }
        );
    }

    private static void SeedClubes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clube>().HasData(
            new Clube
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                UsuarioId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Nome = "Clube Atlético Teste",
                AnoFundacao = 1990,
                CidadeEstado = "São Paulo / SP",
                LigaCompeticao = "Série A",
                BreveHistoria = "Um clube criado para testes.",
                CaminhoEscudo = "https://robohash.org/clube1?set=set1"
            }
        );
    }

    private static void SeedSolicitacoesClubes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SolicitacaoClube>().HasData(
            new SolicitacaoClube
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                JogadorId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ClubeId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Mensagem = "Gostaria de participar da seletiva do Clube Atlético Teste.",
                Status = BaseApi.Domain.Entidades.StatusSolicitacao.Pendente,
                DataSolicitacao = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
