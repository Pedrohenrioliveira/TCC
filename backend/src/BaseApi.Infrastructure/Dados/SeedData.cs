using System;
using System.Collections.Generic;
using System.Linq;
using BaseApi.Domain.Entidades;
using BaseApi.Domain.Enums;

namespace BaseApi.Infrastructure.Dados;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (!context.Perfis.Any())
        {
            context.Perfis.AddRange(
                new Perfil { Id = 1, Nome = "Admin", Descricao = "Administrador do Sistema" },
                new Perfil { Id = 2, Nome = "Jogador", Descricao = "Atleta / Jogador de Futebol" },
                new Perfil { Id = 3, Nome = "Clube", Descricao = "Clube / Equipe / Olheiro" }
            );
            context.SaveChanges();
        }

        if (!context.Usuarios.Any())
        {
            var senhaPadrao = BCrypt.Net.BCrypt.HashPassword("senha123");
            
            // Admin
            var admin = new Usuario { Id = Guid.NewGuid(), NomeCompleto = "Administrador", NomeUsuario = "admin", Email = "admin@tcc.com", SenhaHash = senhaPadrao, PerfilId = 1 };
            context.Usuarios.Add(admin);

            // Dados dos Clubes (agora são 5 clubes para ter várias opções!)
            var clubsData = new[]
            {
                new { user = "realmadrid", name = "Real Madrid C.F.", city = "Madrid, Espanha", est = "Santiago Bernabéu", desc = "Maior clube do mundo." },
                new { user = "corinthians", name = "S.C. Corinthians Paulista", city = "São Paulo, SP", est = "Neo Química Arena", desc = "Bicampeão mundial." },
                new { user = "flamengo", name = "C.R. Flamengo", city = "Rio de Janeiro, RJ", est = "Maracanã", desc = "Maior torcida do mundo." },
                new { user = "barcelona", name = "F.C. Barcelona", city = "Barcelona, Espanha", est = "Camp Nou", desc = "Mais que um clube." },
                new { user = "mancity", name = "Manchester City F.C.", city = "Manchester, Inglaterra", est = "Etihad Stadium", desc = "Potência do futebol inglês." }
            };

            var posicoes = new[] 
            {
                PosicaoJogador.Goleiro, 
                PosicaoJogador.LateralDireito, 
                PosicaoJogador.Zagueiro, PosicaoJogador.Zagueiro, 
                PosicaoJogador.LateralEsquerdo, 
                PosicaoJogador.Volante, PosicaoJogador.Volante, 
                PosicaoJogador.MeioCampo, 
                PosicaoJogador.Ponta, PosicaoJogador.Ponta, 
                PosicaoJogador.Centroavante
            };

            foreach (var cData in clubsData)
            {
                var userClube = new Usuario { Id = Guid.NewGuid(), NomeCompleto = cData.name, NomeUsuario = cData.user, Email = $"{cData.user}@tcc.com", SenhaHash = senhaPadrao, PerfilId = 3 };
                context.Usuarios.Add(userClube);
                
                var clube = new Clube 
                { 
                    Id = Guid.NewGuid(), 
                    UsuarioId = userClube.Id, 
                    Nome = cData.name, 
                    AnoFundacao = 1900, 
                    CidadeEstado = cData.city, 
                    LigaCompeticao = "Série A", 
                    EstadioPrincipal = cData.est, 
                    BreveHistoria = cData.desc
                };
                context.Clubes.Add(clube);

                // 11 jogadores por clube
                for (int i = 0; i < 11; i++)
                {
                    var pos = posicoes[i];
                    var username = $"{cData.user}_jog{i+1}";
                    var userJog = new Usuario { Id = Guid.NewGuid(), NomeCompleto = $"Jogador {i+1} - {cData.name}", NomeUsuario = username, Email = $"{username}@tcc.com", SenhaHash = senhaPadrao, PerfilId = 2 };
                    context.Usuarios.Add(userJog);

                    var jog = new Jogador 
                    { 
                        Id = Guid.NewGuid(), 
                        UsuarioId = userJog.Id, 
                        NomeCompleto = $"Jogador {i+1} ({pos})", 
                        DataNascimento = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc), 
                        PePreferencial = PePreferencial.Direito, 
                        Altura = 180, 
                        Peso = 75, 
                        PosicaoPrincipal = pos, 
                        ClubeId = clube.Id,
                        BioHistorico = $"Atua como {pos} no {cData.name}." 
                    };
                    context.Jogadores.Add(jog);
                }
            }

            context.SaveChanges();

            // 5. Campeonatos
            if (!context.Campeonatos.Any())
            {
                var campeonato = new Campeonato
                {
                    Id = Guid.NewGuid(),
                    Nome = "Copa TCC 2026",
                    Local = "São Paulo, SP",
                    DataInicio = DateTime.UtcNow.AddDays(10),
                    DataFim = DateTime.UtcNow.AddMonths(1),
                    Status = StatusCampeonato.Aberto,
                    TaxaInscricao = 150.00m,
                    ChavePix = "12345678909",
                    DiasDosJogos = "Sábados e Domingos",
                    Descricao = "Campeonato de demonstração do TCC.",
                    LimiteEquipes = 8
                };
                context.Campeonatos.Add(campeonato);

                // Inscrever os 5 clubes no campeonato
                var todosClubes = context.Clubes.ToList();
                foreach (var clubeEntity in todosClubes)
                {
                    var inscricao = new InscricaoCampeonato
                    {
                        Id = Guid.NewGuid(),
                        CampeonatoId = campeonato.Id,
                        ClubeId = clubeEntity.Id,
                        Status = StatusInscricao.Aprovada,
                        AceitouRegulamento = true,
                        NomeResponsavel = "Diretoria do " + clubeEntity.Nome,
                        TelefoneResponsavel = "11999999999",
                        DataResposta = DateTime.UtcNow
                    };
                    context.InscricoesCampeonatos.Add(inscricao);
                }

                context.SaveChanges();
            }
        }
    }
}
