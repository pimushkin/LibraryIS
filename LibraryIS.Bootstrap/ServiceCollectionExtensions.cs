using LibraryIS.Bootstrap.Modules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryIS.Bootstrap
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterPersistence(this IServiceCollection services, WebHostBuilderContext context)
        {
            PersistenceModule.RegisterModule(services, context, context.Configuration);
        }

        public static void RegisterApplication(this IServiceCollection services, WebHostBuilderContext context)
        {
            ApplicationModule.RegisterModule(services, context, context.Configuration);
        }
    }
}