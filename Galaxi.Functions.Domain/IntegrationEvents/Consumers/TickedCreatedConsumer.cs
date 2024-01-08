using Galaxi.Bus.Message;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Persistence.Repositorys;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Galaxi.Functions.Domain.IntegrationEvents.Consumers
{
    public class TickedCreatedConsumer : IConsumer<TickedCreated>
    {
        private readonly ILogger<TickedCreatedConsumer> _log;
        private readonly IFunctionRepository _repo;
        public TickedCreatedConsumer(ILogger<TickedCreatedConsumer> log, IFunctionRepository repo)
        {
            _log = log;
            _repo = repo;
        }

        public async Task Consume(ConsumeContext<TickedCreated> context)
        {
            //_log.LogInformation("Nuevo evento: Se crea un Ticket}.", context.Message.MovietId);
            //_log.LogWarning("Movie Id: {0}", context.Message.MovietId);

            Function tickedCreated = await _repo.GetFunctionById(context.Message.MovietId);
            if (tickedCreated.NumberOfSeats > 0)
            {
                tickedCreated.NumberOfSeats--;

                _repo.Update(tickedCreated);

                //_log.LogWarning("Numero de sillas disponibles de la funcion actualizada");
            }

            //_log.LogWarning("No hay sillas disponibles en la funcion");

            await _repo.SaveAll();

        }
    }
}
