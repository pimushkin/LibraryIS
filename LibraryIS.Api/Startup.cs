using System;
using System.IO;
using FluentValidation.AspNetCore;
using LibraryIS.CrossCutting.Filters;
using LibraryIS.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LibraryIS.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                    options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation();
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "LibraryIS API",
                        Version = "v1",
                        Description = "API for Library Inforamation System",
                        Contact = new OpenApiContact
                        {
                            Name = "Dmitry Pimushkin",
                            Email = "d.pimushkin@gmail.com"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Apache 2.0",
                            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                        }
                    }
                );
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "LibraryIS.Api.xml"));
            });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryIS API"); });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
