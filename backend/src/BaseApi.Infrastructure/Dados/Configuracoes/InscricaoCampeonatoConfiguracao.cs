using BaseApi.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Infrastructure.Dados.Configuracoes;

public class InscricaoCampeonatoConfiguracao : IEntityTypeConfiguration<InscricaoCampeonato>
{
    public void Configure(EntityTypeBuilder<InscricaoCampeonato> builder)
    {
        builder.ToTable("InscricoesCampeonatos");

        builder.HasKey(i => i.Id);

        builder.HasOne(i => i.Campeonato)
            .WithMany()
            .HasForeignKey(i => i.CampeonatoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Clube)
            .WithMany()
            .HasForeignKey(i => i.ClubeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
