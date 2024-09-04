using AutoMapper;
using FluentValidation;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Domain.DTOs;
using Galaxi.Functions.Domain.Infrastructure.Commands;
using Galaxi.Functions.Persistence.Repositorys;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Galaxi.Functions.Domain.Handlers
{
    public class CreatedFunctionHandler
         : IRequestHandler<CreatedFunctionCommand, FunctionSummaryDto>
    {
        private readonly IFunctionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatedFunctionHandler> _log;
        private readonly IValidator<Function> _validatorAvailableMovie;

        public CreatedFunctionHandler
            (
                IFunctionRepository repo,
                IMapper mapper,
                ILogger<CreatedFunctionHandler> log,
                IValidator<Function> validatorAvailableMovie
            )
        {
            _repo = repo;
            _mapper = mapper;
            _log = log;
            _validatorAvailableMovie = validatorAvailableMovie;
        }
        public async Task<FunctionSummaryDto> Handle(CreatedFunctionCommand request, CancellationToken cancellationToken)
        {
            Function createdFunctionMovie = _mapper.Map<Function>(request);

            var result = await _validatorAvailableMovie.ValidateAsync(createdFunctionMovie);

            if (!result.IsValid)
            {
                throw new KeyNotFoundException();
            }

            _repo.Add(createdFunctionMovie);

            var sucess = await _repo.SaveAll();
            if (!sucess)
            {
                throw new InvalidOperationException();
            }
            var functionCreated = _mapper.Map<FunctionSummaryDto>(createdFunctionMovie);
            return functionCreated;
        }
    }
}
