﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SendeYaz.API.Installers
{
    public interface IInstaller
    {
        void InstallSerive(IServiceCollection services,IConfiguration configuration);
        void InstallConfigure(IApplicationBuilder app);
    }
}
