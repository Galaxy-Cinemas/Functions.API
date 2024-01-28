using Galaxi.Functions.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Functions.Domain.Infrastructure.Queries
{
    public record GetFunctionsByIdQuery(int functionId) : IRequest<FunctionDto>;
}
