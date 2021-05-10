namespace Neo.ClrCounters.GC
{
    public interface IGarbageCollectionEventHandler
    {
        void Handle(GarbageCollectionArgs args);
    }
}