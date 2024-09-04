namespace Galaxi.Functions.Data.Models
{
    public class Function
    {
        public Guid FunctionId { get; set; }
        public Guid MovieId { get; set; }
        public Decimal Price { get; set; }
        public DateTime FunctionDate { get; set; }
        public int Room { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
