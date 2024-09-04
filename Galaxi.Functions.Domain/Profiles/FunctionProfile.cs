using AutoMapper;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Domain.DTOs;
using Galaxi.Functions.Domain.Infrastructure.Commands;

namespace Galaxi.Functions.Domain.Profiles
{
    public class FunctionProfile : Profile
    {
        public FunctionProfile()
        {
            CreateMap<Function, FunctionDetailsDto>();
            CreateMap<Function, FunctionSummaryDto>();

            CreateMap<CreatedFunctionCommand, Function>();
            CreateMap<UpdateFunctionCommand, Function>();

        }
    }
}
