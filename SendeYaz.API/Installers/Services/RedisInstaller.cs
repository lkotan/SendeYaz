using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendeYaz.Core.Plugins.Caching;
using SendeYaz.Core.Plugins.Caching.Options;
using SendeYaz.Core.Plugins.Caching.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendeYaz.API.Installers.Services
{
    public class RedisInstaller : IInstaller
    {
        public void InstallConfigure(IApplicationBuilder app)
        {
        }

        public void InstallSerive(IServiceCollection services, IConfiguration configuration)
        {
            var options = new RedisOptions();
            configuration.GetSection(nameof(RedisOptions)).Bind(options);
            if (options.Enabled)
            {
                services.AddSingleton(options);
                services.AddSingleton<RedisServerService>();
                services.AddSingleton<IRedisService, RedisService>();
                services.AddSingleton<ICacheService, RedisCacheService>();
            }
        }
    }
}
