using Microsoft.Diagnostics.Tracing.Analysis.GC;

namespace Neo.ClrCounters.GC
{
    internal class GarbageCollectionArgsMapper : IGarbageCollectionArgsMapper
    {
        public GarbageCollectionArgs Map(int processId, TraceGC gc)
        {
            var sizesBefore = GetGenerationSizes(gc, true);
            var sizesAfter = GetGenerationSizes(gc, false);
            return new GarbageCollectionArgs(
                processId,
                gc.StartRelativeMSec,
                gc.Number,
                gc.Generation,
                (GarbageCollectionReason) gc.Reason,
                (GarbageCollectionType) gc.Type,
                !gc.IsNotCompacting(),
                gc.HeapStats.GenerationSize0,
                gc.HeapStats.GenerationSize1,
                gc.HeapStats.GenerationSize2,
                gc.HeapStats.GenerationSize3,
                sizesBefore,
                sizesAfter,
                gc.SuspendDurationMSec,
                gc.PauseDurationMSec,
                gc.BGCFinalPauseMSec
            );
        }

        private static long[] GetGenerationSizes(TraceGC gc, bool before)
        {
            var sizes = new long[4];
            if (gc.PerHeapHistories == null)
                return sizes;

            foreach (var history in gc.PerHeapHistories)
            {
                for (var gen = 0; gen <= 3; gen++)
                    sizes[gen] += before ? history.GenData[gen].ObjSpaceBefore : history.GenData[gen].ObjSizeAfter;
            }

            return sizes;
        }
    }
}