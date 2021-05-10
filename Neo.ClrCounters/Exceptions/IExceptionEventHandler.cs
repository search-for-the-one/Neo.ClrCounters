namespace Neo.ClrCounters.Exceptions
{
    public interface IExceptionEventHandler
    {
        void Handle(ExceptionArgs args);
    }
}