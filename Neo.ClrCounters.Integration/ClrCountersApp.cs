using System;
using System.Threading;
using System.Threading.Tasks;
using Neo.ConsoleApp.DependencyInjection;

namespace Neo.ClrCounters.Integration
{
    public class ClrCountersApp : IConsoleApp
    {
        private readonly IClrEventProcessor clrEventProcessor;

        public ClrCountersApp(IClrEventProcessor clrEventProcessor) => this.clrEventProcessor = clrEventProcessor;

        public async Task Run()
        {
            StartClrCounters();
            var gcTask = RunGarbageCollection();
            await RunException();
            await gcTask;
        }

        private void StartClrCounters() => new Thread(() => clrEventProcessor.Start()) {Priority = ThreadPriority.Highest, IsBackground = true}.Start();

        private static async Task RunGarbageCollection()
        {
            while (true)
            {
                System.GC.Collect();
                await Task.Delay(10000);
            }
        }

        private static async Task RunException()
        {
            while (true)
            {
                try
                {
                    throw new Exception("Exception Message");
                }
                catch (Exception)
                {
                    // ignored
                }

                await Task.Delay(10000);
            }
        }
    }
}