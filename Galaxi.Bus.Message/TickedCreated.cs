namespace Galaxi.Bus.Message
{
    public class TickedCreated
    {
        public TickedCreated(int functionId)
        {
            FunctionId = functionId;
        }

        public int FunctionId { get; }
    }
}
