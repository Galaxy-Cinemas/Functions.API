using Galaxi.Functions.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Galaxi.Functions.Persistence.Configurations
{
    public class FunctionConfig : IEntityTypeConfiguration<Function>
    {

        public void Configure(EntityTypeBuilder<Function> builder)
        {
            builder
                .ToTable("MovieFunction", "DBO")
                .HasKey(x => new { x.FunctionId });
        }
    }
}
