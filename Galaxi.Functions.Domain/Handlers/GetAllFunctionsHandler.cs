using AutoMapper;
using Galaxi.Functions.Domain.DTOs;
using Galaxi.Functions.Domain.Infrastructure.Queries;
using Galaxi.Functions.Persistence.Repositorys;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Galaxi.Functions.Domain.Handlers
{
    internal class GetAllFunctionsHandler
        : IRequestHandler<GetAllFunctionsQuery, IEnumerable<FunctionSummaryDto>>
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
        public async Task<IEnumerable<FunctionSummaryDto>> Handle(GetAllFunctionsQuery request, CancellationToken cancellationToken)
        {
            var function = await _repo.GetFunctionsAsync();
            if (function == null || !function.Any())
            {
                throw new KeyNotFoundException();
            }
            var functionViewModel = _mapper.Map<List<FunctionSummaryDto>>(function);
            return functionViewModel;
        }
    }
}
