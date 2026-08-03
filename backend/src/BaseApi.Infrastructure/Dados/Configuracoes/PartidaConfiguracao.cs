using BaseApi.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Infrastructure.Dados.Configuracoes;

public class PartidaConfiguracao : IEntityTypeConfiguration<Partida>
{
    public void Configure(EntityTypeBuilder<Partida> builder)
    {
        builder.ToTable("Partidas");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Local)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.DataHora)
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired();

        builder.HasOne(p => p.Rodada)
            .WithMany()
            .HasForeignKey(p => p.RodadaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.ClubeMandante)
            .WithMany()
            .HasForeignKey(p => p.ClubeMandanteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.ClubeVisitante)
            .WithMany()
            .HasForeignKey(p => p.ClubeVisitanteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
