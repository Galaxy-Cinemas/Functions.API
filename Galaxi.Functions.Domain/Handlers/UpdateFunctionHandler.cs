using AutoMapper;
using Galaxi.Functions.Domain.Infrastructure.Commands;
using Galaxi.Functions.Persistence.Repositorys;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Galaxi.Functions.Domain.Handlers
{
    public class UpdateFunctionHandler
        : IRequestHandler<UpdateFunctionCommand, Unit>
    {
        private readonly IFunctionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatedFunctionHandler> _log;

        public UpdateFunctionHandler(IFunctionRepository repo, IMapper mapper, ILogger<CreatedFunctionHandler> log)
        {
            _repo = repo;
            _mapper = mapper;
            _log = log;
        }

        public async Task<Unit> Handle(UpdateFunctionCommand request, CancellationToken cancellationToken)
        {
            var existingFunctionMovie = await _repo.GetFunctionById(request.FunctionId);
            if (existingFunctionMovie == null)
            {
                throw new KeyNotFoundException();
            }
            _mapper.Map(request, existingFunctionMovie);
            _repo.Update(existingFunctionMovie);

            var sucess = await _repo.SaveAll();
            if (!sucess)
            {
                throw new InvalidOperationException();
            }

            return Unit.Value;
        }
    }
}
