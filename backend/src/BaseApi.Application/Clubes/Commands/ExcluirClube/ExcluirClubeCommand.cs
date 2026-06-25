using MediatR;
using System;

namespace BaseApi.Application.Clubes.Commands.ExcluirClube;

public record ExcluirClubeCommand(Guid Id) : IRequest;
