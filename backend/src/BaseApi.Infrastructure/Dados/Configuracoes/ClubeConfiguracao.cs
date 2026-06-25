using BaseApi.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Infrastructure.Dados.Configuracoes;

public class ClubeConfiguracao : IEntityTypeConfiguration<Clube>
{
    public void Configure(EntityTypeBuilder<Clube> builder)
    {
        builder.ToTable("clubes");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.CaminhoEscudo)
            .HasMaxLength(500);

        builder.Property(c => c.AnoFundacao)
            .IsRequired();

        builder.Property(c => c.CidadeEstado)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.LigaCompeticao)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.EstadioPrincipal)
            .HasMaxLength(150);

        builder.Property(c => c.BreveHistoria)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasIndex(c => c.Nome).IsUnique();
    }
}
