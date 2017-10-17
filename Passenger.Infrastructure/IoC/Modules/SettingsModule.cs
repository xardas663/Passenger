using Autofac;
using Microsoft.Extensions.Configuration;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Modules;
using Passenger.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Passenger.Infrastructure.Extensions;

namespace Passenger.Infrastructure.IoC.Modules
{
    public class SettingsModule:Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<JwtSettings>())
                .SingleInstance();
        }
    
    }
}
