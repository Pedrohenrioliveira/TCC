using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Domain.Entidades;
using BaseApi.Domain.Interfaces.Repositorios;
using BaseApi.Infrastructure.Dados;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Infrastructure.Repositorios;

public class CampeonatoRepositorio(AppDbContext context) : ICampeonatoRepositorio
{
    public async Task<IEnumerable<Campeonato>> ListarAsync(string? status, CancellationToken ct)
    {
        var query = context.Campeonatos.AsQueryable();

        if (!string.IsNullOrEmpty(status))
        {
            if (status.ToLower() == "aberto")
                query = query.Where(c => c.Status == StatusCampeonato.Aberto);
            else if (status.ToLower() == "andamento")
                query = query.Where(c => c.Status == StatusCampeonato.EmAndamento);
            else if (status.ToLower() == "finalizado")
                query = query.Where(c => c.Status == StatusCampeonato.Finalizado);
        }

        return await query.OrderBy(c => c.DataInicio).ToListAsync(ct);
    }
}
