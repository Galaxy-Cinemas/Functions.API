using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Functions.Domain.Infrastructure.Commands
{
    public record UpdateFunctionCommand(int FunctionId,int MovieId, Decimal Price, DateTime FunctionDate, int Room, int NumberOfSeats)
   : IRequest<bool>;
}
