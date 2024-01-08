using Galaxi.Functions.Data.Models;

namespace Galaxi.Functions.Persistence.Repositorys
{
    public interface IFunctionRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<Function> GetFunctionById(int id);
        Task<IEnumerable<Function>> GetFunctionsAsync();
        Task<bool> SaveAll();
        void Update<T>(T entity) where T : class;
    }
}