using LibraryIS.Core.Interfaces;
using LibraryIS.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryIS.Bootstrap.Modules
{
    public class PersistenceModule
    {
        public static void RegisterModule(IServiceCollection services, WebHostBuilderContext context, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
