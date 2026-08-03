using BaseApi.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Infrastructure.Dados.Configuracoes;

public class ClassificacaoConfiguracao : IEntityTypeConfiguration<Classificacao>
{
    public void Configure(EntityTypeBuilder<Classificacao> builder)
    {
        builder.ToTable("Classificacoes");

        builder.HasKey(c => c.Id);

        // Ensure unique classification per club per championship
        builder.HasIndex(c => new { c.CampeonatoId, c.ClubeId }).IsUnique();

        builder.Property(c => c.Pontos).IsRequired().HasDefaultValue(0);
        builder.Property(c => c.PartidasJogadas).IsRequired().HasDefaultValue(0);
        builder.Property(c => c.Vitorias).IsRequired().HasDefaultValue(0);
        builder.Property(c => c.Empates).IsRequired().HasDefaultValue(0);
        builder.Property(c => c.Derrotas).IsRequired().HasDefaultValue(0);
        builder.Property(c => c.GolsPro).IsRequired().HasDefaultValue(0);
        builder.Property(c => c.GolsContra).IsRequired().HasDefaultValue(0);

        builder.Ignore(c => c.SaldoGols);

        builder.HasOne(c => c.Campeonato)
            .WithMany()
            .HasForeignKey(c => c.CampeonatoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Clube)
            .WithMany()
            .HasForeignKey(c => c.ClubeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
