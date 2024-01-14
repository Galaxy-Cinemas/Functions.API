using AutoMapper;
using Galaxi.Functions.Domain.DTOs;
using Galaxi.Functions.Domain.Infrastructure.Queries;
using Galaxi.Functions.Persistence.Repositorys;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Functions.Domain.Handlers
{
    internal class GetAllFunctionsHandler
        : IRequestHandler<GetAllFunctionsQuery, IEnumerable<FunctionDto>>
    {
        private readonly IFunctionRepository _repo;
        private readonly IMapper _mapper;
        public GetAllFunctionsHandler(IFunctionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FunctionDto>> Handle(GetAllFunctionsQuery request, CancellationToken cancellationToken)
        {
            var function = await _repo.GetFunctionsAsync();

            var functionViewModel = _mapper.Map<List<FunctionDto>>(function);

            return functionViewModel;
        }
    }
}
