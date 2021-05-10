using Microsoft.Diagnostics.Tracing.Analysis.GC;

namespace Neo.ClrCounters.GC
{
    internal interface IGarbageCollectionEventProcessor
    {
        bool Enabled { get; }
        void Process(int processId, TraceGC gc);
    }
}