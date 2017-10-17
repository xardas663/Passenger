using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Passenger.Api.Controllers
{
    public class DriverController : ApiControllerBase
    {       
        private readonly IDriverService _driverService;

        public DriverController(ICommandDispatcher commandDispatcher, IDriverService driverService):base(commandDispatcher)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drivers = await _driverService.BrowseAsync();
            return Json(drivers);
        }

        [HttpPost]       
        public async Task<IActionResult> Post([FromBody]ChangeUserPassword command)
        {
            await CommandDispatcher.DispatcherAsync(command);
            return NoContent();
        }
    }
}
