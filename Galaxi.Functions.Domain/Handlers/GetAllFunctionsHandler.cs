using AutoMapper;
using Galaxi.Functions.Domain.DTOs;
using Galaxi.Functions.Domain.Infrastructure.Queries;
using Galaxi.Functions.Persistence.Repositorys;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Galaxi.Functions.Domain.Handlers
{
    internal class GetAllFunctionsHandler
        : IRequestHandler<GetAllFunctionsQuery, IEnumerable<FunctionDto>>
    {
        private readonly IFunctionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatedFunctionHandler> _log;

        public GetAllFunctionsHandler(IFunctionRepository repo, IMapper mapper, ILogger<CreatedFunctionHandler> log)
        {
            _repo = repo;
            _mapper = mapper;
            _log = log;
        }
        public async Task<IEnumerable<FunctionDto>> Handle(GetAllFunctionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var function = await _repo.GetFunctionsAsync();
                var functionViewModel = _mapper.Map<List<FunctionDto>>(function);
                return functionViewModel;
            }
            catch (Exception ex)
            {
                _log.LogError("An exception has occurred getting all movies {0}", ex.Message);
                throw;
            }
        }
    }
}
