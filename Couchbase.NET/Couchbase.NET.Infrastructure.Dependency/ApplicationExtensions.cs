using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Couchbase.NET.Infrastructure.Dependency
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder ConfigureCouchbase(this IApplicationBuilder app, IHostApplicationLifetime appLifetime)
        {
            appLifetime.ApplicationStopped.Register(() =>
            {
                app.ApplicationServices.GetRequiredService<ICouchbaseLifetimeService>().Close();
            });

            return app;
        }
    }
}
