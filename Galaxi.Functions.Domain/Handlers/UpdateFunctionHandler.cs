using AutoMapper;
using Galaxi.Functions.Data.Models;
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
    public class UpdateFunctionHandler
        : IRequestHandler<UpdateFunctionCommand, bool>
    {
        private readonly IFunctionRepository _repo;
        private readonly IMapper _mapper;
        public UpdateFunctionHandler(IFunctionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateFunctionCommand request, CancellationToken cancellationToken)
        {
            var updateFunction = _mapper.Map<Function>(request);

            _repo.Update(updateFunction);

            return await _repo.SaveAll();
        }
    }
}
