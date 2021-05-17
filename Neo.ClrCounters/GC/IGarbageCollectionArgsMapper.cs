using Microsoft.Diagnostics.Tracing.Analysis.GC;

namespace Neo.ClrCounters.GC
{
    internal interface IGarbageCollectionArgsMapper
    {
        GarbageCollectionArgs Map(int processId, TraceGC gc);
    }
}