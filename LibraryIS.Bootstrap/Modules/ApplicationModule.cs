using LibraryIS.Application.Behaviors;
using LibraryIS.Application.Interfaces;
using LibraryIS.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryIS.Bootstrap.Modules
{
    public class ApplicationModule
    {
        public static void RegisterModule(IServiceCollection services, WebHostBuilderContext context, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IBooksCatalogService, BooksCatalogService>();
            services.AddScoped<IEvaluationService, EvaluationService>();
            services.AddScoped<IBookService, BookService>();
        }
    }
}
