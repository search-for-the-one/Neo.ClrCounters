using Microsoft.Diagnostics.Tracing.Parsers.Clr;

namespace Neo.ClrCounters.Exceptions
{
    internal interface IExceptionEventProcessor
    {
        bool Enabled { get; }
        void Process(ExceptionTraceData data);
    }
}