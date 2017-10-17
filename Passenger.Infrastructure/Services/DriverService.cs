using Passenger.Core.Repositories;
using System;
using Passenger.Infrastructure.DTO;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using AutoMapper;
using System.Collections.Generic;

namespace Passenger.Infrastructure.Services
{

    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public DriverService(IDriverRepository driverRepository, IUserRepository userRepository,IMapper mapper)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DriverDto>> BrowseAsync()
        {
            var drivers = await _driverRepository.BrowseAsync();
            return _mapper.Map<IEnumerable<Driver>, IEnumerable<DriverDto>>(drivers);
        }

        public async Task CreateAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"user with {userId} was not found");
            }

            var driver = await _driverRepository.GetAsync(userId);
            if (driver != null)
            {
                throw new Exception($"driver with {driver.UserId} already exists");
            }
            driver = new Driver(user);
            await _driverRepository.AddAsync(driver);
            


        }

        public async Task <DriverDto> GetAsync(Guid userID)
        {
            var driver = await _driverRepository.GetAsync(userID);
            return _mapper.Map<Driver, DriverDto>(driver);
            
        }

        public async Task SetVehicleAsync(Guid userId, string brand, string name, int seats)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"driver with {driver.UserId} was not found");
            }
            driver.SetVehicle(brand, seats, brand);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}