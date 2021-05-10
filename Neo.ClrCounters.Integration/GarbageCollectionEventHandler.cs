using System;
using Neo.ClrCounters.GC;

namespace Neo.ClrCounters.Integration
{
    public class GarbageCollectionEventHandler : IGarbageCollectionEventHandler
    {
        public void Handle(GarbageCollectionArgs args) => Console.WriteLine($"{args.ProcessId} {nameof(args.Generation)}:{args.Generation}");
    }
}