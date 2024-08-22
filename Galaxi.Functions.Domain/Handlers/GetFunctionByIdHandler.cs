using AutoMapper;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Domain.DTOs;
using Galaxi.Functions.Domain.Infrastructure.Queries;
using Galaxi.Functions.Persistence.Repositorys;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Galaxi.Functions.Domain.Handlers
{
    public class GetFunctionByIdHandler
         : IRequestHandler<GetFunctionsByIdQuery, FunctionDto>
    {
        private readonly IFunctionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetFunctionByIdHandler> _log;

        public GetFunctionByIdHandler(IFunctionRepository repo, IMapper mapper, ILogger<GetFunctionByIdHandler> log)
        {
            _repo = repo;
            _mapper = mapper;
            _log = log;
        }

        public async Task<FunctionDto> Handle(GetFunctionsByIdQuery request, CancellationToken cancellationToken)
        {
            Function functionById = await _repo.GetFunctionById(request.functionId);
            if (functionById == null)
            {
                throw new KeyNotFoundException();
            }
            var functionByIdViewModel = _mapper.Map<FunctionDto>(functionById);
            return functionByIdViewModel;
        }
    }
}
