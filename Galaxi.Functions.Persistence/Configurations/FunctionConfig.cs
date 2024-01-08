using Galaxi.Functions.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
