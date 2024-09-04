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

            #region HasData

            builder.HasData(
                new Function
                {
                    FunctionId = Guid.NewGuid(),
                    MovieId = Guid.NewGuid(),
                    Price = 10.50m,
                    FunctionDate = new DateTime(2024, 9, 3, 18, 30, 0),
                    Room = 1,
                    NumberOfSeats = 100
                },
                new Function
                {
                    FunctionId = Guid.NewGuid(),
                    MovieId = Guid.NewGuid(),
                    Price = 12.00m,
                    FunctionDate = new DateTime(2024, 9, 3, 21, 00, 0),
                    Room = 2,
                    NumberOfSeats = 120
                },
                new Function
                {
                    FunctionId = Guid.NewGuid(),
                    MovieId = Guid.NewGuid(),
                    Price = 15.00m,
                    FunctionDate = new DateTime(2024, 9, 4, 15, 00, 0),
                    Room = 3,
                    NumberOfSeats = 150
                },
                new Function
                {
                    FunctionId = Guid.NewGuid(),
                    MovieId = Guid.NewGuid(),
                    Price = 8.00m,
                    FunctionDate = new DateTime(2024, 9, 5, 11, 00, 0),
                    Room = 4,
                    NumberOfSeats = 80
                },
                new Function
                {
                    FunctionId = Guid.NewGuid(),
                    MovieId = Guid.NewGuid(),
                    Price = 18.50m,
                    FunctionDate = new DateTime(2024, 9, 6, 20, 00, 0),
                    Room = 5,
                    NumberOfSeats = 200
                }

                );

            #endregion
        }
    }
}
