using Microsoft.Extensions.DependencyInjection;
using SendeYaz.Core.Plugins.Authentication;
using SendeYaz.Core.Plugins.Authentication.Jwt;
using SendeYaz.Core.Plugins.Caching;
using SendeYaz.Core.Plugins.Caching.Redis;
using SendeYaz.Core.Repositories;
using SendeYaz.Core.Repositories.EF;
using SendeYaz.Core.Utilities.IoC;
using StackExchange.Redis;
using System.Diagnostics;

namespace SendeYaz.Core.Extenstions
{
    public static class ServiceInstallerExtensions
    {
        public static void InstallCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<ITokenHelper, JwtHelper>();
            services.AddSingleton<IUserService, UserJwtService>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer
            //  .Connect("localhost:6379"));

            //services.AddSingleton<IRedisCache, RedisCache>();

            services.AddSingleton<Stopwatch>();

            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<,>));
            services.AddTransient(typeof(IDataAccessRepository<>), typeof(EfDataAccessRepository<>));


            ServiceTool.Create(services);
        }
    }
}
