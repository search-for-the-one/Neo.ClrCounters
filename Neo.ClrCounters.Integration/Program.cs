using System.Threading.Tasks;

namespace Neo.ClrCounters.Integration
{
    internal static class Program
    {
        public static async Task Main() => await new Startup().RunAsync<ClrCountersApp>();
    }
}