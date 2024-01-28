using AutoMapper;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Domain.DTOs;
using Galaxi.Functions.Domain.Infrastructure.Queries;
using Galaxi.Functions.Persistence.Repositorys;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                Function functionById = await _repo.GetFunctionById(request.functionId);
                var functionByIdViewModel = _mapper.Map<FunctionDto>(functionById);
                return functionByIdViewModel;
            }
            catch (Exception ex)
            {
                _log.LogError("An exception has occurred getting the movie function {0}", ex.Message);
                throw;
            }
        }
    }
}
