using Couchbase.Extensions.DependencyInjection;
using Couchbase.NET.Application.Interfaces;
using Couchbase.NET.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Reflection;

namespace Couchbase.NET.Infrastructure.Data.Dependency
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            var repoTypes = Assembly.Load(typeof(Repositories.CountryRepository).Assembly.GetName())
                          .GetTypes()
                          .Where(x => !string.IsNullOrEmpty(x.Namespace))
                          .Where(x => x.IsClass)
                          .Where(x => typeof(IRepository).IsAssignableFrom(x))
                          .Select(x => new
                          {
                              Interface = x.GetInterface($"I{x.Name}"),
                              Implementation = x
                          })
                          .ToList();

            foreach (var repoType in repoTypes)
            {
                services.AddScoped(repoType.Interface, repoType.Implementation);
            }

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            var serviceTypes = Assembly.Load(typeof(Application.Services.CountryService).Assembly.GetName())
                          .GetTypes()
                          .Where(x => !string.IsNullOrEmpty(x.Namespace))
                          .Where(x => x.IsClass)
                          .Where(x => typeof(IService).IsAssignableFrom(x))
                          .Select(x => new
                          {
                              Interface = x.GetInterface($"I{x.Name}"),
                              Implementation = x
                          })
                          .ToList();

            foreach (var serviceType in serviceTypes)
            {
                services.AddScoped(serviceType.Interface, serviceType.Implementation);
            }

            services.AddMediatR(typeof(Application.Common.ServiceResult).Assembly);

            return services;
        }

        public static IServiceCollection AddCouchbaseServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCouchbase(options =>
            {
                configuration.GetSection("Couchbase").Bind(options);
                //options.AddLinq();
            }).AddCouchbaseBucket<INamedBucketProvider>("default");

            return services;
        }

        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Couchbase.NET", Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Application.ViewModels.MapperProfiles.StockProfile),
                typeof(Application.ViewModels.MapperProfiles.CountryProfile));

            return services;
        }

        public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Couchbase.NET v1"));

            return app;
        }
    }
}
