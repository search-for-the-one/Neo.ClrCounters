using System;
using Neo.ClrCounters.Exceptions;

namespace Neo.ClrCounters.Integration
{
    public class ExceptionEventHandler : IExceptionEventHandler
    {
        public void Handle(ExceptionArgs args) => Console.WriteLine($"{args.TimeStamp} {args.ProcessId} {args.TypeName} {args.Message}");
    }
}