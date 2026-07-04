using MediatR;
using System;

namespace BaseApi.Application.Ligas.Queries.ObterTabelaClassificacao;

public record ObterTabelaClassificacaoQuery(Guid LigaId) : IRequest<TabelaClassificacaoDto>;
