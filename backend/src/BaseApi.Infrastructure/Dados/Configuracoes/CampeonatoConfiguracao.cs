using BaseApi.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Infrastructure.Dados.Configuracoes;

public class CampeonatoConfiguracao : IEntityTypeConfiguration<Campeonato>
{
    public void Configure(EntityTypeBuilder<Campeonato> builder)
    {
        builder.ToTable("Campeonatos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome).IsRequired().HasMaxLength(200);
        
        builder.Property(c => c.Local).HasMaxLength(200);
        
        builder.Property(c => c.CaminhoLogo).HasMaxLength(1000);
        
        builder.Property(c => c.ChavePix).HasMaxLength(200);
        
        builder.Property(c => c.DiasDosJogos).HasMaxLength(200);
        
        builder.Property(c => c.CaminhoImagemCampo).HasMaxLength(1000);
        
        builder.Property(c => c.TaxaInscricao).HasColumnType("decimal(18,2)");
        
        builder.Property(c => c.Descricao).HasColumnType("text");
    }
}
