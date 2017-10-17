using Passenger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Core.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryDriverRepository : IDriverRepository
    {
        private static ISet<Driver> _drivers = new HashSet<Driver>();

        public async Task AddAsync(Driver driver)
        {
            await Task.FromResult(_drivers.Add(driver));
        }

        public async Task<Driver> GetAsync(Guid userId)
            => await Task.FromResult(_drivers.FirstOrDefault(x => x.UserId == userId));

        public async Task<IEnumerable<Driver>> BrowseAsync()
            => await Task.FromResult(_drivers);
       

        public async Task UpdateAsync(Driver driver)
        {
            await Task.CompletedTask;
        }
    }
}
