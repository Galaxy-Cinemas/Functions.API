using MediatR;

namespace Galaxi.Functions.Domain.Infrastructure.Commands
{
    public record CreatedFunctionCommand(int MovieId, Decimal Price, DateTime FunctionDate, int Room, int NumberOfSeats)
    : IRequest<bool>;
}
