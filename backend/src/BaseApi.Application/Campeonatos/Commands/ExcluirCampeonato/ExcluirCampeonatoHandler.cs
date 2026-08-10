using BaseApi.Application.Comum.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Campeonatos.Commands.ExcluirCampeonato;

public class ExcluirCampeonatoHandler : IRequestHandler<ExcluirCampeonatoCommand, bool>
{
    private readonly IAppDbContext _context;

    public ExcluirCampeonatoHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ExcluirCampeonatoCommand request, CancellationToken cancellationToken)
    {
        var campeonato = await _context.Campeonatos
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (campeonato == null)
            return false;

        _context.Campeonatos.Remove(campeonato);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
