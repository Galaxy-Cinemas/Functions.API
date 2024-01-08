using Galaxi.Functions.Domain.Infrastructure.Commands;
using Galaxi.Functions.Domain.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Galaxi.Functions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FunctionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var functions = await _mediator.Send(new GetAllFunctionsQuery());
            return Ok(functions);
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
                return Ok(updateFunction);

            return BadRequest();
        }
    }
}
