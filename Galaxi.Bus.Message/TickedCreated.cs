namespace Galaxi.Bus.Message
{
    public class TickedCreated
    {
        public TickedCreated(int movietId)
        {
            MovietId = movietId;
        }

        public int MovietId { get; }
    }
}
