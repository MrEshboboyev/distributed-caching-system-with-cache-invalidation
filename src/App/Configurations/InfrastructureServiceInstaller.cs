using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Security;
using Scrutor;
using StackExchange.Redis;

namespace App.Configurations;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .Scan(
                selector => selector
                    .FromAssemblies(
                        AssemblyReference.Assembly,
                        Persistence.AssemblyReference.Assembly)
                    .AddClasses(false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime());
        
        // Add Redis
        var redisConnection = configuration.GetConnectionString("Redis");
        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(redisConnection!));
        
        // Register Caching dependencies
        services.AddScoped<ICacheRepository, RedisCacheRepository>();  // Ensure CacheRepository is implemented
        services.AddScoped<ICacheStrategy, CacheStrategy>();
    }
}