using Microsoft.Extensions.DependencyInjection;
using UtsDB.Application;
using UtsDB.Application.Engine;
using UtsDB.Application.Services;
using UtsDB.Domain.Config;
using UtsDB.Domain.Interfaces;
using UtsDB.Domain.Services;

namespace Utsd.Console;

public class ServerBootstrapper
{
    public static IServiceProvider Configure(DatabaseConfig config)
    {
        var services = new ServiceCollection();
        config.InitPaths();
        services.AddSingleton(config);
        services.AddSingleton<IMetadataService, MetadataService>();
        services.AddSingleton<IShardFactory, ShardFactory>();
        services.AddSingleton<IStrategyFactory, StrategyFactory>();

        services.AddTransient<FrontEnd>();

        return services.BuildServiceProvider();
    }
}