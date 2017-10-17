using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService:IService
    {
        Task<DriverDto> GetAsync(Guid userID);
        Task<IEnumerable<DriverDto>> BrowseAsync();

        Task CreateAsync(Guid userId);

        Task SetVehicleAsync(Guid userId, string brand, string name, int seats);
      
     
    }
}
