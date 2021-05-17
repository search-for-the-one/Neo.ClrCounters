namespace Neo.ClrCounters
{
    public interface IClrEventProcessor
    {
        void Start(int processId = 0);
    }
}