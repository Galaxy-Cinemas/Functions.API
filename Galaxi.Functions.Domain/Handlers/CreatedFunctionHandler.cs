using AutoMapper;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Domain.Infrastructure.Commands;
using Galaxi.Functions.Persistence.Repositorys;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Galaxi.Functions.Domain.Handlers
{
    public class CreatedFunctionHandler
         : IRequestHandler<CreatedFunctionCommand, bool>
    {
        private readonly IFunctionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatedFunctionHandler> _log;

        public CreatedFunctionHandler(IFunctionRepository repo, IMapper mapper, ILogger<CreatedFunctionHandler> log)
        {
            _repo = repo;
            _mapper = mapper;
            _log = log;
        }
        public async Task<bool> Handle(CreatedFunctionCommand request, CancellationToken cancellationToken)
        {
            var createdFunctionMovie = _mapper.Map<Function>(request);
            _repo.Add(createdFunctionMovie);

            var sucess = await _repo.SaveAll();
            if (!sucess)
            {
                throw new InvalidOperationException();
            }
            return sucess;
        }
    }
}
