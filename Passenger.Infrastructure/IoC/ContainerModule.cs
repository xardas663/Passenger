using Autofac;
using Microsoft.Extensions.Configuration;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.Mappers;
using Passenger.Infrastructure.Modules;
using Passenger.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.IoC
{
    public class ContainerModule:Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                   .SingleInstance();
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule <ServiceModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule(new SettingsModule(_configuration));
        }
    }
}
