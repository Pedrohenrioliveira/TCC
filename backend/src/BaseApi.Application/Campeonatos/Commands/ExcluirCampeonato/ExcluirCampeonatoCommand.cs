using MediatR;
using System;

namespace BaseApi.Application.Campeonatos.Commands.ExcluirCampeonato;

public class ExcluirCampeonatoCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
