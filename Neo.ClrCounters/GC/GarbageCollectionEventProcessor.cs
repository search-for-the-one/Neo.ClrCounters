using System.Collections.Generic;
using System.Linq;
using Microsoft.Diagnostics.Tracing.Analysis.GC;

namespace Neo.ClrCounters.GC
{
    internal class GarbageCollectionEventProcessor : IGarbageCollectionEventProcessor
    {
        private readonly IEnumerable<IGarbageCollectionEventHandler> handlers;
        private readonly IGarbageCollectionArgsMapper mapper;

        public GarbageCollectionEventProcessor(IEnumerable<IGarbageCollectionEventHandler> handlers, IGarbageCollectionArgsMapper mapper)
        {
            this.handlers = handlers;
            this.mapper = mapper;
            Enabled = this.handlers.Any();
        }

        public bool Enabled { get; }

        public void Process(int processId, TraceGC gc)
        {
            var args = mapper.Map(processId, gc);
            foreach (var handler in handlers)
                handler.Handle(args);
        }
    }
}