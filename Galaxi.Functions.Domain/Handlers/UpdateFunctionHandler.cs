using AutoMapper;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Domain.Infrastructure.Commands;
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
    public class UpdateFunctionHandler
        : IRequestHandler<UpdateFunctionCommand, bool>
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

        public async Task<bool> Handle(UpdateFunctionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updateFunction = _mapper.Map<Function>(request);
                _repo.Update(updateFunction);
                return await _repo.SaveAll();
            }
            catch (Exception ex)
            {
                _log.LogError("An exception occurred, the movie function could not be updated {0}", ex.Message);
                return false;
            }
          
        }
    }
}
