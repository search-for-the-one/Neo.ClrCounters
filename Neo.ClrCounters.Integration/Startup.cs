using Microsoft.Extensions.DependencyInjection;
using Neo.ConsoleApp.DependencyInjection;

namespace Neo.ClrCounters.Integration
{
    public class Startup : ConsoleAppStartup
    {
        protected override void ConfigureServices(IServiceCollection services) =>
            services.AddClrCounters()
                .AddGarbageCollection<GarbageCollectionEventHandler>()
                .AddException<ExceptionEventHandler>();
    }
}