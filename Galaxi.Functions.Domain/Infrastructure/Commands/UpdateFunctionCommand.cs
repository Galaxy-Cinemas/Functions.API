using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Functions.Domain.Infrastructure.Commands
{
    public record UpdateFunctionCommand(Guid FunctionId, Guid MovieId, Decimal Price, DateTime FunctionDate, int Room, int NumberOfSeats)
   : IRequest<Unit>;
}
