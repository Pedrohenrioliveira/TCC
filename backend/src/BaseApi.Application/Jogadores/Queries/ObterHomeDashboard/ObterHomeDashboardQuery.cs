using MediatR;
using System;

namespace BaseApi.Application.Jogadores.Queries.ObterHomeDashboard;

public record ObterHomeDashboardQuery(Guid Id) : IRequest<HomeDashboardDto>;
