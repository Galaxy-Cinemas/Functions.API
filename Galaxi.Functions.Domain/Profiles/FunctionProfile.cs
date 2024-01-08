using AutoMapper;
using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Domain.DTOs;

namespace Galaxi.Functions.Domain.Profiles
{
    public class FunctionProfile : Profile
    {
        public FunctionProfile()
        {
            CreateMap<Function, FunctionDto>();
        }
    }
}
