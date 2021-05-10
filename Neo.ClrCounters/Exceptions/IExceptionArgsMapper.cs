using Microsoft.Diagnostics.Tracing.Parsers.Clr;

namespace Neo.ClrCounters.Exceptions
{
    internal interface IExceptionArgsMapper
    {
        ExceptionArgs Map(ExceptionTraceData data);
    }
}