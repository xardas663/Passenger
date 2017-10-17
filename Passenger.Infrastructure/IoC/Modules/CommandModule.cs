using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Passenger.Infrastructure.Handlers.Users;
using Passenger.Infrastructure.Commands;
using System.Reflection;

namespace Passenger.Infrastructure.Modules
{
    public class CommandModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                   .AsClosedTypesOf(typeof(ICommandHandler<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}
