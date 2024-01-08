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

    }
}
