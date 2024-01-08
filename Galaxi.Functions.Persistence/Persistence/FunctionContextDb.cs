using Galaxi.Functions.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Galaxi.Functions.Persistence.Persistence
{
    public class FunctionContextDb : DbContext, IFunctionContextDb
    {
        private readonly IConfiguration? _configuration;

        public FunctionContextDb()
        { }

        public FunctionContextDb(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            base.ChangeTracker.LazyLoadingEnabled = false;
            _configuration = configuration;
        }
        public DbSet<Function> MovieFunction { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
