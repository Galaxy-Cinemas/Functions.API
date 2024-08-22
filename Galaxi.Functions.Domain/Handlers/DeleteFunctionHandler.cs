using Galaxi.Functions.Domain.Infrastructure.Commands;
using Galaxi.Functions.Persistence.Repositorys;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Galaxi.Functions.Domain.Handlers
{
    public class DeleteFunctionHandler
        : IRequestHandler<DeleteFunctionCommand, Unit>
    {
        private readonly IFunctionRepository _repo;
        private readonly ILogger<CreatedFunctionHandler> _log;

        public DeleteFunctionHandler(IFunctionRepository repo, ILogger<CreatedFunctionHandler> log)
        {
            _repo = repo;
            _log = log;
        }
        public async Task<Unit> Handle(DeleteFunctionCommand request, CancellationToken cancellationToken)
        {
            var existingFunctionMovie = await _repo.GetFunctionByIdAsync(request.functionId);
            if (existingFunctionMovie == null)
            {
                throw new KeyNotFoundException();
            }
            await _repo.Delete(existingFunctionMovie);
            var sucess = await _repo.SaveAll();

            if (!sucess)
            {
                throw new InvalidOperationException();
            }

            return Unit.Value;
        }
    }
}
