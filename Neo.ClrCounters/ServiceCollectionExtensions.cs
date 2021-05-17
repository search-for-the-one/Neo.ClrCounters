using Microsoft.Extensions.DependencyInjection;
using Neo.ClrCounters.Exceptions;
using Neo.ClrCounters.GC;

namespace Neo.ClrCounters
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClrCounters(this IServiceCollection services)
        {
            services.AddSingleton<IClrEventProcessor, ClrEventProcessor>();

            services.AddSingleton<IGarbageCollectionEventProcessor, GarbageCollectionEventProcessor>();
            services.AddSingleton<IGarbageCollectionArgsMapper, GarbageCollectionArgsMapper>();

            services.AddSingleton<IExceptionEventProcessor, ExceptionEventProcessor>();
            services.AddSingleton<IExceptionArgsMapper, ExceptionArgsMapper>();

            return services;
        }

        public static IServiceCollection AddGarbageCollection<T>(this IServiceCollection services) where T : class, IGarbageCollectionEventHandler =>
            services.AddSingleton<IGarbageCollectionEventHandler, T>();

        public static IServiceCollection AddException<T>(this IServiceCollection services) where T : class, IExceptionEventHandler =>
            services.AddSingleton<IExceptionEventHandler, T>();
    }
}