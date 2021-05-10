using System;
using System.Diagnostics.Tracing;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Analysis;
using Microsoft.Diagnostics.Tracing.Parsers;
using Neo.ClrCounters.Exceptions;
using Neo.ClrCounters.GC;

namespace Neo.ClrCounters
{
    internal class ClrEventProcessor : IClrEventProcessor
    {
        private readonly IExceptionEventProcessor exceptionEventProcessor;
        private readonly IGarbageCollectionEventProcessor garbageCollectionEventProcessor;
        private int selectedProcessId;

        public ClrEventProcessor(IGarbageCollectionEventProcessor garbageCollectionEventProcessor, IExceptionEventProcessor exceptionEventProcessor)
        {
            this.garbageCollectionEventProcessor = garbageCollectionEventProcessor;
            this.exceptionEventProcessor = exceptionEventProcessor;
        }

        public void Start(int processId = 0)
        {
            selectedProcessId = processId == 0 ? Environment.ProcessId : processId;

            var client = new DiagnosticsClient(selectedProcessId);
            using var eventPipeSession = client.StartEventPipeSession(GetProvider());
            using var source = new EventPipeEventSource(eventPipeSession.EventStream);
            RegisterListeners(source);
            // this is a blocking call
            source.Process();
        }

        private EventPipeProvider GetProvider() => new("Microsoft-Windows-DotNETRuntime", keywords: (long) GetKeywords(), eventLevel: EventLevel.Informational);

        private ClrTraceEventParser.Keywords GetKeywords()
        {
            ClrTraceEventParser.Keywords keywords = 0;
            if (garbageCollectionEventProcessor.Enabled)
                keywords |= ClrTraceEventParser.Keywords.GC;
            if (exceptionEventProcessor.Enabled)
                keywords |= ClrTraceEventParser.Keywords.Exception;
            return keywords;
        }

        private void RegisterListeners(TraceEventDispatcher source)
        {
            if (garbageCollectionEventProcessor.Enabled)
                RegisterGarbageCollection(source);

            if (exceptionEventProcessor.Enabled)
                RegisterException(source);
        }

        private void RegisterGarbageCollection(TraceEventDispatcher source)
        {
            source.NeedLoadedDotNetRuntimes();
            source.AddCallbackOnProcessStart(proc =>
            {
                if (proc.ProcessID != selectedProcessId)
                    return;

                proc.AddCallbackOnDotNetRuntimeLoad(runtime =>
                {
                    runtime.GCEnd += (p, gc) => { garbageCollectionEventProcessor.Process(selectedProcessId, gc); };
                });
            });
        }

        private void RegisterException(TraceEventDispatcher source) =>
            source.Clr.ExceptionStart += data =>
            {
                if (selectedProcessId == data.ProcessID)
                    exceptionEventProcessor.Process(data);
            };
    }
}