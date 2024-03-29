﻿using Galaxi.Functions.Domain.Infrastructure.Commands;
using Galaxi.Functions.Domain.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Galaxi.Functions.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("v1/[action]")]
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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _log.LogInformation("Get all movie functions");
                var functions = await _mediator.Send(new GetAllFunctionsQuery());
                return Ok(functions);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                GetFunctionsByIdQuery functionById = new GetFunctionsByIdQuery(functionId: id);

                _log.LogInformation("Get function {0}", id);
                var function = await _mediator.Send(functionById);
                return Ok(function);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetByMovieId(int movieId)
        {
            try
            {
                GetFunctionByMovieIdQuery functionById = new GetFunctionByMovieIdQuery(movieId: movieId);

                _log.LogInformation("Get function {0}", movieId);
                var function = await _mediator.Send(functionById);
                return Ok(function);
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
                return Ok(functionToCreate);

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
                _log.LogWarning("Movie function has been update, with functionId {0}", id);
                return Ok(updateFunction);
            }
            _log.LogWarning("Movie function could not be update, with functionId {0}", id);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            DeleteFunctionCommand functionId = new DeleteFunctionCommand(functionId:id);
            var delete = await _mediator.Send(functionId);

            if (delete)
            {
                _log.LogWarning("Movie function has been removed, with functionId {0}", id);
                return Ok();
            }
            _log.LogWarning("Movie function could not be removed, with functionId {0}", id);
            return BadRequest();

        }
    }
}
