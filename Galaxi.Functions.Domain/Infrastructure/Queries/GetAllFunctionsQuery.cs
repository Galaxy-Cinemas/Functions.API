using Galaxi.Functions.Domain.DTOs;
using MediatR;

namespace Galaxi.Functions.Domain.Infrastructure.Queries
{
    public record GetAllFunctionsQuery : IRequest<IEnumerable<FunctionDto>>;

}
