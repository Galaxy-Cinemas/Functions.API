using Galaxi.Functions.Data.Models;

namespace Galaxi.Functions.Persistence.Repositorys
{
    public interface IFunctionRepository
    {
        Task Add(Function function);
        Task Delete(Function function);
        Task Update(Function function);
        Task<Function> GetFunctionByIdAsync(int id);
        Task<IEnumerable<Function>> GetFunctionByMovieIdAsync(int id);
        Task<IEnumerable<Function>> GetFunctionsAsync();
        Task<bool> SaveAll();
    }
}