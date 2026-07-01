using BaseApi.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Infrastructure.Dados.Configuracoes;

public class JogadorConfiguracao : IEntityTypeConfiguration<Jogador>
{
    public void Configure(EntityTypeBuilder<Jogador> builder)
    {
        builder.ToTable("jogadores");
        builder.HasKey(j => j.Id);

        builder.Property(j => j.CaminhoFoto)
            .HasMaxLength(500);

        builder.Property(j => j.NomeCompleto)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(j => j.DataNascimento)
            .IsRequired();

        builder.Property(j => j.PePreferencial)
            .IsRequired();

        builder.Property(j => j.Altura)
            .IsRequired();

        builder.Property(j => j.Peso)
            .IsRequired();

        builder.Property(j => j.PosicaoPrincipal)
            .IsRequired();

        builder.Property(j => j.PosicaoSecundaria);

        builder.Property(j => j.BioHistorico)
            .IsRequired()
            .HasMaxLength(1000);

        // Relacionamento Opcional 1:N com Clube
        builder.HasOne(j => j.Clube)
               .WithMany()
               .HasForeignKey(j => j.ClubeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
