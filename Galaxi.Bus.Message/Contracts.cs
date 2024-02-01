namespace Galaxi.Bus.Message
{
    public record TickedCreated
    {
        public int FunctionId { get; init; }
        public int NumSeat { get; init; }
        public string Email { get; init; }
    }
    public record CheckFunctionSeats
    {
        public int FunctionId { get; init; }
    }

    public record FunctionStatusSeats
    {
        public bool Exist { get; init; }
        public int NumSeatAvailable { get; init; }
    }
}
