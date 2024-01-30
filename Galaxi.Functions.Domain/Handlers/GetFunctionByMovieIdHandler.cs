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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Galaxi.Functions.Domain.Handlers
{
    public class GetFunctionByMovieIdHandler
            : IRequestHandler<GetFunctionByMovieIdQuery, IEnumerable<FunctionDto>>
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

        public async Task<IEnumerable<FunctionDto>> Handle(GetFunctionByMovieIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var functionByMovieId = await _repo.GetFunctionByMovieId(request.movieId);
                var functionViewModel = _mapper.Map<List<FunctionDto>>(functionByMovieId);
                return functionViewModel;
            }
            catch (Exception ex)
            {
                _log.LogError("An exception has occurred getting the movie function {0}", ex.Message);
                throw;
            }
        }

      
    }
}
