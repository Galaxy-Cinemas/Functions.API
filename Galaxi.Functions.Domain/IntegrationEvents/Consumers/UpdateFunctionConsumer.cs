using Galaxi.Bus.Message;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Persistence.Repositorys;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Galaxi.Functions.Domain.IntegrationEvents.Consumers
{
    public class UpdateFunctionConsumer : IConsumer<TickedCreated>
    {
        private readonly ILogger<UpdateFunctionConsumer> _log;
        private readonly IFunctionRepository _repo;
        public UpdateFunctionConsumer(ILogger<UpdateFunctionConsumer> log, IFunctionRepository repo)
        {
            _log = log;
            _repo = repo;
        }

        public async Task Consume(ConsumeContext<TickedCreated> context)
        {
            _log.LogInformation(" --- new event update available seat");
            try 
            {
                _log.LogInformation(" --- Function Id: {0}", context.Message.FunctionId);
                Function tickedCreated = await _repo.GetFunctionById(context.Message.FunctionId);
                _log.LogInformation(" --- Retrieve movie function from DB");
                

                if (tickedCreated.NumberOfSeats > context.Message.NumSeat)
                {
                    for (int i = 0; i < context.Message.NumSeat; i++)
                    {
                        tickedCreated.NumberOfSeats--;
                    }
                    _repo.Update(tickedCreated);
                }
                await _repo.SaveAll();
                _log.LogWarning(" --- Number of chairs available for the updated function");
            }
            catch (Exception ex) 
            {
                _log.LogError("An exception occurred while updating the number of available chairs. {0}", ex.Message);
            }
        }
    }
}
