using Autofac;
using Passenger.Infrastructure.Commands;
using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Handlers.Users
{
   

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;
        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }
        public async Task DispatcherAsync<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command),$"Command: '{typeof(T).Name}' cannot be null.");
            }
            var handler = _context.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}