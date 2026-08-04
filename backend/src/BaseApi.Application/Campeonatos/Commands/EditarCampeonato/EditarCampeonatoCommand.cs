using MediatR;
using System;

namespace BaseApi.Application.Campeonatos.Commands.EditarCampeonato;

public class EditarCampeonatoCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Local { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public int LimiteEquipes { get; set; }
}
