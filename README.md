# Neo.ClrCounters

With this library, you can trace [CLR](https://docs.microsoft.com/en-us/dotnet/standard/clr) events (garbage collection and exception), with [Event Pipe](https://docs.microsoft.com/en-us/dotnet/core/diagnostics/eventpipe).

The original idea is from [here](https://github.com/chrisnas/ClrEvents).

## How to use this library?

- Add nuget library `Neo2.ClrCounters` to your project.
- Add `services.AddClrCounters()` to enable CLR Counters.
- Create class `GarbageCollectionEventHandler` and implement interface `IGarbageCollectionEventHandler`.
- Add `services.AddGarbageCollection<GarbageCollectionEventHandler>()` to enable garbage collection events monitor.
- Create class `ExceptionEventHandler` and implement interface `IExceptionEventHandler`.
- Add `services.AddGarbageCollection<ExceptionEventHandler>()` to enable exception events monitor.
- You can create and add multiple garbage collection and exception event handler class.
- Start `ClrEventProcessor` with following code.

```
    public class ClrCountersApp : IConsoleApp
    {
        private readonly IClrEventProcessor clrEventProcessor;

        public ClrCountersApp(IClrEventProcessor clrEventProcessor) => this.clrEventProcessor = clrEventProcessor;

        public async Task Run() => StartClrCounters();

        private void StartClrCounters() => 
            new Thread(() => clrEventProcessor.Start()) {Priority = ThreadPriority.Highest, IsBackground = true}.Start();
    }
```