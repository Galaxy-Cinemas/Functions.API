using FluentValidation;
using Galaxi.Bus.Message;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Domain.DTOs;
using MassTransit;

namespace Galaxi.Functions.Domain.IntegrationEvents.Validators
{
    public class ValidatorAvailableMovie : AbstractValidator<Function>
    {
        private readonly IRequestClient<CheckAvailableMovie> _client;
        private MovieStatus? _status;

        public ValidatorAvailableMovie(IRequestClient<CheckAvailableMovie> client)
        {
            _client = client;

            RuleFor(x => x.MovieId).NotEmpty().MustAsync(Exists);
        }
        private async Task<bool> Exists(Guid MovieId, CancellationToken cancellationToken)
        {
            var response = await _client.GetResponse<MovieStatus>(new CheckAvailableMovie
            {
                MovieId = MovieId
            }, cancellationToken);
            _status = response.Message;
            return _status.Exist;
        }
    }
}
