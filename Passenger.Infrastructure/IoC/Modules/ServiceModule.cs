﻿using Autofac;
using Passenger.Infrastructure.Modules;
using Passenger.Infrastructure.Services;
using System.Reflection;

namespace Passenger.Infrastructure.IoC.Modules
{

    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                    .Where(x => x.IsAssignableTo<IService>())
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .SingleInstance();

            builder.RegisterType<JwtHandler>()
                .As<IJwtHandler>()
                .SingleInstance();
        }
    }
}