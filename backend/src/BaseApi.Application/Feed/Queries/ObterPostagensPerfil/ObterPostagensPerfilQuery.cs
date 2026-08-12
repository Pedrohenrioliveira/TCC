using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Feed.Queries.ObterPostagensPerfil;

public class ObterPostagensPerfilQuery : IRequest<List<PostagemDto>>
{
    public Guid PerfilId { get; set; }

    public ObterPostagensPerfilQuery(Guid perfilId)
    {
        PerfilId = perfilId;
    }
}

public class ObterPostagensPerfilQueryHandler(IAppDbContext context) : IRequestHandler<ObterPostagensPerfilQuery, List<PostagemDto>>
{
    public async Task<List<PostagemDto>> Handle(ObterPostagensPerfilQuery request, CancellationToken cancellationToken)
    {
        var postagens = await context.Postagens
            .AsNoTracking()
            .Where(p => p.JogadorId == request.PerfilId || p.ClubeId == request.PerfilId)
            .OrderByDescending(p => p.DataPostagem)
            .Select(p => new PostagemDto
            {
                Id = p.Id,
                CaminhoFoto = p.CaminhoFoto,
                Descricao = p.Descricao,
                DataPostagem = p.DataPostagem
            })
            .ToListAsync(cancellationToken);

        return postagens;
    }
}
