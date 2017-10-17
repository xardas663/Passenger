using Microsoft.AspNetCore.Authorization;
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
    public class AccountController : ApiControllerBase
    {
        private readonly IJwtHandler _jwtHandler;
       

        public AccountController(ICommandDispatcher commandDispatcher, IJwtHandler jwtHandler):base(commandDispatcher)
        {
            _jwtHandler = jwtHandler;
        }

        [HttpGet("")]
        [Authorize]
        [Route("auth")]
        public IActionResult GetAuth()
        {
            return Content("auth");
        }

        

        [HttpPut("")]
        [Route("password")]
        public async Task<IActionResult> Post([FromBody]ChangeUserPassword command)
        {
            await CommandDispatcher.DispatcherAsync(command);
            return NoContent();
        }
    }
}
