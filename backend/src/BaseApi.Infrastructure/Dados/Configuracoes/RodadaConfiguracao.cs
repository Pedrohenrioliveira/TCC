using BaseApi.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Infrastructure.Dados.Configuracoes;

public class RodadaConfiguracao : IEntityTypeConfiguration<Rodada>
{
    public void Configure(EntityTypeBuilder<Rodada> builder)
    {
        builder.ToTable("Rodadas");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Numero)
            .IsRequired();

        builder.Property(r => r.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(r => r.DataInicio)
            .IsRequired();

        builder.Property(r => r.DataFim)
            .IsRequired();

        builder.HasOne(r => r.Campeonato)
            .WithMany() // Assuming we don't have a list of Rodadas in Campeonato yet
            .HasForeignKey(r => r.CampeonatoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
