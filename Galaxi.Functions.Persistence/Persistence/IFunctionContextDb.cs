using Galaxi.Functions.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Galaxi.Functions.Persistence.Persistence
{
    public interface IFunctionContextDb
    {
        DbSet<Function> MovieFunction { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}