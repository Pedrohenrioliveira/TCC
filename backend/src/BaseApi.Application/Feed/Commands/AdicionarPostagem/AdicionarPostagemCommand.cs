using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Feed.Commands.AdicionarPostagem;

public class AdicionarPostagemCommand : IRequest<RespostaApi<Guid>>
{
    public Guid PerfilId { get; set; }
    public string CaminhoFoto { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}

public class AdicionarPostagemCommandHandler(IAppDbContext context) : IRequestHandler<AdicionarPostagemCommand, RespostaApi<Guid>>
{
    public async Task<RespostaApi<Guid>> Handle(AdicionarPostagemCommand request, CancellationToken cancellationToken)
    {
        // Verifica se é um Clube ou Jogador baseado no PerfilId associado.
        // Como o request pede apenas PerfilId, podemos inferir se pertence a Clube ou Jogador.
        // No contexto do TCC atual, Jogadores têm UsuarioId/ClubeId, mas se recebermos o Id direto do Clube ou Jogador, fica mais fácil.
        // Para simplificar e bater com a regra de negócios (O usuário logado é dono de um perfil), 
        // vamos verificar de quem é esse Id.

        var jogador = await context.Jogadores.FirstOrDefaultAsync(j => j.Id == request.PerfilId, cancellationToken);
        var clube = await context.Clubes.FirstOrDefaultAsync(c => c.Id == request.PerfilId, cancellationToken);

        if (jogador == null && clube == null)
        {
            return RespostaApi<Guid>.Falha("Perfil não encontrado.");
        }

        var postagem = new Postagem
        {
            CaminhoFoto = request.CaminhoFoto,
            Descricao = request.Descricao,
            JogadorId = jogador?.Id,
            ClubeId = clube?.Id
        };

        context.Postagens.Add(postagem);
        await context.SaveChangesAsync(cancellationToken);

        return RespostaApi<Guid>.Sucesso(postagem.Id, "Postagem realizada com sucesso!");
    }
}
