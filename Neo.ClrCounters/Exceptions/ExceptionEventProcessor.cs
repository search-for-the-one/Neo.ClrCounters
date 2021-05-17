using System.Collections.Generic;
using System.Linq;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;

namespace Neo.ClrCounters.Exceptions
{
    internal class ExceptionEventProcessor : IExceptionEventProcessor
    {
        private readonly IEnumerable<IExceptionEventHandler> handlers;
        private readonly IExceptionArgsMapper mapper;

        public ExceptionEventProcessor(IEnumerable<IExceptionEventHandler> handlers, IExceptionArgsMapper mapper)
        {
            this.handlers = handlers;
            this.mapper = mapper;
            Enabled = this.handlers.Any();
        }

        public bool Enabled { get; }

        public void Process(ExceptionTraceData data)
        {
            var args = mapper.Map(data);
            foreach (var handler in handlers)
                handler.Handle(args);
        }
    }
}