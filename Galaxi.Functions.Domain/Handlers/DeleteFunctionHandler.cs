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
    public class DeleteFunctionHandler
        : IRequestHandler<DeleteFunctionCommand, bool>
    {
        private readonly IFunctionRepository _repo;
        public DeleteFunctionHandler(IFunctionRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(DeleteFunctionCommand request, CancellationToken cancellationToken)
        {
            var function = await _repo.GetFunctionById(request.functionId);
            _repo.Delete(function);
            return await _repo.SaveAll();
        }
    }
}
