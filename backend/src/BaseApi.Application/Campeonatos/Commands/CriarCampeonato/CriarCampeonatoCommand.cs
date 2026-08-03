using System;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Application.Comum.Interfaces;
using BaseApi.Application.Comum.Modelos;
using BaseApi.Domain.Entidades;
using MediatR;

namespace BaseApi.Application.Campeonatos.Commands.CriarCampeonato;

public class CriarCampeonatoCommand : IRequest<RespostaApi<Guid>>
{
    public string Nome { get; set; } = string.Empty;
    public string Local { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public int LimiteEquipes { get; set; }
    public string CaminhoLogo { get; set; } = string.Empty;
}

public class CriarCampeonatoCommandHandler(IAppDbContext dbContext) : IRequestHandler<CriarCampeonatoCommand, RespostaApi<Guid>>
{
    public async Task<RespostaApi<Guid>> Handle(CriarCampeonatoCommand request, CancellationToken cancellationToken)
    {
        var campeonato = new Campeonato
        {
            Nome = request.Nome,
            Local = request.Local,
            DataInicio = request.DataInicio,
            DataFim = request.DataFim,
            LimiteEquipes = request.LimiteEquipes,
            CaminhoLogo = request.CaminhoLogo,
            Status = StatusCampeonato.Aberto
        };

        dbContext.Campeonatos.Add(campeonato);
        await dbContext.SaveChangesAsync(cancellationToken);

        return RespostaApi<Guid>.Sucesso(campeonato.Id, "Campeonato criado com sucesso.");
    }
}
