using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using LibraryIS.Bootstrap;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using static LibraryIS.CrossCutting.Extensions.AssemblyExtensions;

[assembly: HostingStartup(typeof(HostingInitialization))]
namespace LibraryIS.Bootstrap
{
    public class HostingInitialization : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            var apiAssembly = GetAssemblyByName("LibraryIS.Api");
            var applicationAssembly = GetAssemblyByName("LibraryIS.Application");
            builder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom
                .Configuration(hostingContext.Configuration).Enrich.FromLogContext().WriteTo.Debug().WriteTo
                .Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"));
            builder.ConfigureServices((context, services) =>
            {
                services.RegisterPersistence(context);
                services.RegisterApplication(context);
                services.AddAutoMapper(apiAssembly, applicationAssembly);
                services.AddValidatorsFromAssembly(applicationAssembly);
                services.AddMediatR(applicationAssembly);
            });
        }
    }
}
