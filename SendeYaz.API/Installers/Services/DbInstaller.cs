using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendeYaz.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendeYaz.API.Installers.Services
{
    public class DbInstaller : IInstaller
    {
        public void InstallConfigure(IApplicationBuilder app)
        {
        }

        public void InstallSerive(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SendeYazContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Api"));
            });
            //services.AddTransient<SendeYazContext>();
            services.AddTransient<DbContext, SendeYazContext>();
        }
    }
}
