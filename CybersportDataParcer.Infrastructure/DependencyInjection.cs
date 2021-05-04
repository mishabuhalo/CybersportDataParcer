using CybersportDataParcer.Infrastructure.Services;
using CybersportDataParser.Application.Interfaces;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace CybersportDataParcer.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICSGOMatchesParser, CSGOMatchesParser>();
            services.AddTransient<IDotaMatchesParser, DotaMatchesParser>();
            services.AddTransient<ILoLMatchesParser, LoLMatchesParser>();

            var builder = services.AddIdentityServer(options =>
            {
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(configuration.GetSection("IdentityServer:Clients").Get<Client[]>());

            builder.AddDeveloperSigningCredential();

            return services;
        }

        public static class Config
        {
            public static IEnumerable<IdentityResource> IdentityResources =>
                new IdentityResource[]
                {
                new IdentityResources.OpenId()
                };

            public static IEnumerable<ApiScope> ApiScopes =>
                new[]
                {
                new ApiScope("parser", "Cybersport parser"),
                };
        }
    }
}
