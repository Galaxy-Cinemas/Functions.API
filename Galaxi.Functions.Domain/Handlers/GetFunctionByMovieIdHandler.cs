using AutoMapper;
using Galaxi.Functions.Domain.DTOs;
using Galaxi.Functions.Domain.Infrastructure.Queries;
using Galaxi.Functions.Persistence.Repositorys;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Galaxi.Functions.Domain.Handlers
{
    public class GetFunctionByMovieIdHandler
            : IRequestHandler<GetFunctionByMovieIdQuery, IEnumerable<FunctionSummaryDto>>
    {
        private readonly IFunctionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetFunctionByMovieIdHandler> _log;

        public GetFunctionByMovieIdHandler(IFunctionRepository repo, IMapper mapper, ILogger<GetFunctionByMovieIdHandler> log)
        {
            _repo = repo;
            _mapper = mapper;
            _log = log;
        }

        public async Task<IEnumerable<FunctionSummaryDto>> Handle(GetFunctionByMovieIdQuery request, CancellationToken cancellationToken)
        {
            var functionByMovieId = await _repo.GetFunctionByMovieIdAsync(request.movieId);
            if (functionByMovieId == null || !functionByMovieId.Any())
            {
                throw new KeyNotFoundException();
            }
            var functionViewModel = _mapper.Map<List<FunctionSummaryDto>>(functionByMovieId);
            return functionViewModel;
        }
    }
}
