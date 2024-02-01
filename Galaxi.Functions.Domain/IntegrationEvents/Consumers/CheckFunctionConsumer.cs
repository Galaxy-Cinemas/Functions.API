using Galaxi.Bus.Message;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Domain.DTOs;
using Galaxi.Functions.Persistence.Repositorys;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Functions.Domain.IntegrationEvents.Consumers
{
    public class CheckFunctionConsumer : IConsumer<CheckFunctionSeats>
    {
        private readonly ILogger<UpdateFunctionConsumer> _log;
        private readonly IFunctionRepository _repo;

        public CheckFunctionConsumer(ILogger<UpdateFunctionConsumer> log, IFunctionRepository repo)
        {
            _log = log;
            _repo = repo;
        }
        public async Task Consume(ConsumeContext<CheckFunctionSeats> context)
        {
            Function function = await _repo.GetFunctionById(context.Message.FunctionId);

            var exist = function != null;
            var seats = exist ? function.NumberOfSeats : 0;

            await context.RespondAsync(new FunctionStatusSeats
            {
                Exist = exist,
                NumSeatAvailable = seats
            });

            
        }
    }
}
