using Galaxi.Functions.Data.Models;
using Galaxi.Functions.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Galaxi.Functions.Persistence.Repositorys
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly FunctionContextDb _context;

        public FunctionRepository(FunctionContextDb context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<Function> GetFunctionById(int id)
        {
            var function = await _context.MovieFunction.FirstOrDefaultAsync(u => u.FunctionId == id);
            return function;
        }

        public async Task<IEnumerable<Function>> GetFunctionsAsync()
        {
            var function = await _context.MovieFunction.ToListAsync();
            return function;
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
