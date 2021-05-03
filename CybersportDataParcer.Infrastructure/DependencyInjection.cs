using CybersportDataParcer.Infrastructure.Services;
using CybersportDataParser.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CybersportDataParcer.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICSGOMatchesParser, CSGOMatchesParser>();
            return services;
        }
    }
}
