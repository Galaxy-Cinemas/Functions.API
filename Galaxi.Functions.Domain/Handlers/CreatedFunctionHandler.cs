using AutoMapper;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Domain.DTOs;
using Galaxi.Functions.Domain.Infrastructure.Commands;
using Galaxi.Functions.Persistence.Repositorys;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Functions.Domain.Handlers
{
    public class CreatedFunctionHandler
         : IRequestHandler<CreatedFunctionCommand, bool>
    {
        private readonly IFunctionRepository _repo;
        private readonly IMapper _mapper;
        public CreatedFunctionHandler(IFunctionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreatedFunctionCommand request, CancellationToken cancellationToken)
        {
            var createdMovie = _mapper.Map<Function>(request);

            _repo.Add(createdMovie);

            return await _repo.SaveAll();
        }
    }
}
