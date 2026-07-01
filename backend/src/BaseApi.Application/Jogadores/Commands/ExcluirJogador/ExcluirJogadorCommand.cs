using MediatR;
using System;

namespace BaseApi.Application.Jogadores.Commands.ExcluirJogador;

public record ExcluirJogadorCommand(Guid Id) : IRequest;
