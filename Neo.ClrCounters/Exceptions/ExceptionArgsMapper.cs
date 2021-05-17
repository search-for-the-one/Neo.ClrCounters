using Microsoft.Diagnostics.Tracing.Parsers.Clr;

namespace Neo.ClrCounters.Exceptions
{
    internal class ExceptionArgsMapper : IExceptionArgsMapper
    {
        public ExceptionArgs Map(ExceptionTraceData data) => new(data.TimeStamp, data.ProcessID, data.ExceptionType, data.ExceptionMessage);
    }
}