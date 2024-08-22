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
            catch (KeyNotFoundException ex)
            {
                _log.LogWarning(ex.Message);
                var response = ResponseHandler<string>.CreateNotFoundResponse("Function not found.", ex.Message);
                return StatusCode(response.StatusCode.Value, response);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
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
            catch (InvalidOperationException ex)
            {
                _log.LogWarning(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("Failed to save changes to the database.", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
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
            catch (InvalidOperationException ex)
            {
                _log.LogWarning(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("Failed to save changes to the database.", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatedFunctionCommand functionToCreate)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var errorResponse = ResponseHandler<string>.CreateErrorResponse("Validation failed", errors);
                _log.LogWarning("The model is not valid for creating a function.", errorResponse);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }

            try
            {
                var created = await _mediator.Send(functionToCreate);
                var successResponse = ResponseHandler<string>.CreateSuccessResponse("Function created successfully", null);
                return StatusCode(successResponse.StatusCode.Value, successResponse);

            }
            catch (InvalidOperationException ex)
            {
                _log.LogWarning(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("Failed to save changes to the database.", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFunctionCommand updateFunction)
        {
            if (id != updateFunction.FunctionId)
            {
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", new List<string> { "The Function ID does not match the film ID." });
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            try
            {
                var functionToUpdate = await _mediator.Send(updateFunction);
                _log.LogWarning("Function has been update, with functionId {0}", id);
                var successResponse = ResponseHandler<UpdateFunctionCommand>.CreateSuccessResponse("Function updated successfully", updateFunction);
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            catch (KeyNotFoundException ex)
            {
                _log.LogWarning(ex.Message);
                var response = ResponseHandler<string>.CreateNotFoundResponse("Function not found.", "The Function with the specified ID does not exist.");
                return StatusCode(response.StatusCode.Value, response);
            }
            catch (InvalidOperationException ex)
            {
                _log.LogWarning(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("Failed to save changes to the database.", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
        }

        [HttpDelete("{functionId}")]
        public async Task<IActionResult> Delete(int functionId)
        {
            try
            {
                var delete = await _mediator.Send(new DeleteFunctionCommand(functionId));
                var successResponse = ResponseHandler<string>.CreateSuccessResponse("Function deleted successfully", null);
                _log.LogInformation("Function deleted successfully");
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            catch (KeyNotFoundException ex)
            {
                _log.LogWarning(ex.Message);
                var response = ResponseHandler<string>.CreateNotFoundResponse("Function not found.", "The Function with the specified ID does not exist.");
                return StatusCode(response.StatusCode.Value, response);
            }
            catch (InvalidOperationException ex)
            {
                _log.LogWarning(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("Failed to save changes to the database.", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }

        }
    }
}
