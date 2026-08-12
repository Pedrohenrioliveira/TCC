using BaseApi.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Infrastructure.Dados.Configuracoes;

public class PostagemConfiguracao : IEntityTypeConfiguration<Postagem>
{
    public void Configure(EntityTypeBuilder<Postagem> builder)
    {
        builder.ToTable("Postagens");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.CaminhoFoto)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.Descricao)
            .HasMaxLength(1000);

        // Relacionamento opcional com Jogador
        builder.HasOne(p => p.Jogador)
            .WithMany()
            .HasForeignKey(p => p.JogadorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento opcional com Clube
        builder.HasOne(p => p.Clube)
            .WithMany()
            .HasForeignKey(p => p.ClubeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
