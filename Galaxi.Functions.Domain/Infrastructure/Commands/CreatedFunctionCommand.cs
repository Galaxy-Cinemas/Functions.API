using Galaxi.Functions.Domain.DTOs;
using MediatR;

namespace Galaxi.Functions.Domain.Infrastructure.Commands
{
    public record CreatedFunctionCommand(Guid MovieId, Decimal Price, DateTime FunctionDate, int Room, int NumberOfSeats)
    : IRequest<FunctionSummaryDto>;
}
