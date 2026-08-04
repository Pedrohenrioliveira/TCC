using BaseApi.Application.Comum.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApi.Application.Campeonatos.Commands.EditarCampeonato;

public class EditarCampeonatoHandler : IRequestHandler<EditarCampeonatoCommand, bool>
{
    private readonly IAppDbContext _context;

    public EditarCampeonatoHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(EditarCampeonatoCommand request, CancellationToken cancellationToken)
    {
        var campeonato = await _context.Campeonatos
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (campeonato == null)
            return false;

        campeonato.Nome = request.Nome;
        campeonato.Local = request.Local;
        campeonato.DataInicio = request.DataInicio;
        campeonato.DataFim = request.DataFim;
        campeonato.LimiteEquipes = request.LimiteEquipes;
        campeonato.AtualizadoEm = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
