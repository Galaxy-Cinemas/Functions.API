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
    public class DeleteFunctionHandler
        : IRequestHandler<DeleteFunctionCommand, bool>
    {
        private readonly IFunctionRepository _repo;
        private readonly ILogger<CreatedFunctionHandler> _log;

        public DeleteFunctionHandler(IFunctionRepository repo, ILogger<CreatedFunctionHandler> log)
        {
            _repo = repo;
            _log = log;
        }
        public async Task<bool> Handle(DeleteFunctionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var function = await _repo.GetFunctionById(request.functionId);
                _repo.Delete(function);
                return await _repo.SaveAll();
            }
            catch (Exception ex)
            {
                _log.LogError("An exception occurred, the movie function could not be removed {0}", ex.Message);
                return false;
            }
        }
    }
}
