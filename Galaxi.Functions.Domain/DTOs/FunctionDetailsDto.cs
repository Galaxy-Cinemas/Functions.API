namespace Galaxi.Functions.Domain.DTOs
{
    public class FunctionDetailsDto
    {
        public Guid FunctionId { get; set; }
        public Guid MovieId { get; set; }
        public Decimal Price { get; set; }
        public DateTime FunctionDate { get; set; }
        public int Room { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
