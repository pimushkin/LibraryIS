using System;
using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using LibraryIS.Bootstrap;
using LibraryIS.CrossCutting.Configuration;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using static LibraryIS.CrossCutting.Extensions.AssemblyExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
                var authOptionsSection = context.Configuration.GetSection(AuthenticationOptions.JsonKey);
                services.Configure<AuthenticationOptions>(authOptionsSection);
                var authOptions = authOptionsSection.Get<AuthenticationOptions>();
                services.AddHttpContextAccessor();
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authOptions.Issuer,
                        ValidAudience = authOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
                services.RegisterPersistence(context);
                services.RegisterApplication(context);
                services.AddAutoMapper(apiAssembly, applicationAssembly);
                services.AddValidatorsFromAssembly(applicationAssembly);
                services.AddMediatR(applicationAssembly);
            });
        }
    }
}
