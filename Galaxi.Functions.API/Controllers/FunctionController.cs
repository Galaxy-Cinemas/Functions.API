using Galaxi.Functions.Domain.Infrastructure.Commands;
using Galaxi.Functions.Domain.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Galaxi.Functions.Domain.Response;
using Galaxi.Functions.Domain.DTOs;

namespace Galaxi.Functions.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("[action]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private readonly ILogger<FunctionController> _log;
        private readonly IMediator _mediator;

        public FunctionController(ILogger<FunctionController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _log.LogInformation("Get all movie functions");
                var functions = await _mediator.Send(new GetAllFunctionsQuery());
                var successResponse = ResponseHandler<IEnumerable<FunctionDto>>.CreateSuccessResponse("Functions retrieved successfully", functions);
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{functionId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int functionId)
        {
            try
            {
                _log.LogInformation("Get function {0}", functionId);
                var functionById = await _mediator.Send(new GetFunctionsByIdQuery(functionId));
                var successResponse = ResponseHandler<FunctionDto>.CreateSuccessResponse("Function by id retrieved successfully", functionById);
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{movieId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByMovieId(int movieId)
        {
            try
            {
                _log.LogInformation("Get function {0}", movieId);
                var functionByMovieId = await _mediator.Send(new GetFunctionByMovieIdQuery(movieId));

                var successResponse = ResponseHandler<IEnumerable<FunctionDto>>.CreateSuccessResponse("Function by movie id retrieved successfully", functionByMovieId);
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatedFunctionCommand functionToCreate)
        {
            var created = await _mediator.Send(functionToCreate);
            if (created)
            {
                var successResponse = ResponseHandler<string>.CreateSuccessResponse("Function created successfully", null);
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateFunctionCommand updateFunction)
        {
            if (id != updateFunction.FunctionId)
            {
                return BadRequest();
            }

            var functionToUpdate = await _mediator.Send(updateFunction);

            if (functionToUpdate)
            {
                _log.LogWarning("Function has been update, with functionId {0}", id);
                var successResponse = ResponseHandler<UpdateFunctionCommand>.CreateSuccessResponse("Function updated successfully", updateFunction);
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            _log.LogWarning("Function could not be update, with functionId {0}", id);
            return BadRequest();
        }

        [HttpDelete("{functionId}")]
        public async Task<IActionResult> Delete(int functionId)
        {
            var delete = await _mediator.Send(new DeleteFunctionCommand(functionId));

            if (delete)
            {
                var successResponse = ResponseHandler<string>.CreateSuccessResponse("Function deleted successfully", null);
                _log.LogInformation("Function deleted successfully");
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            _log.LogWarning("Function could not be removed, with functionId {0}", functionId);
            return BadRequest();

        }
    }
}
