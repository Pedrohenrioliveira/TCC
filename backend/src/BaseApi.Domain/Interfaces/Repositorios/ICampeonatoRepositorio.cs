using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseApi.Domain.Entidades;

namespace BaseApi.Domain.Interfaces.Repositorios;

public interface ICampeonatoRepositorio
{
    Task<IEnumerable<Campeonato>> ListarAsync(string? status, CancellationToken ct);
}
