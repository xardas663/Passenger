﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Handlers.Users;
using Passenger.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;

namespace Passenger.Api.Controllers
{
 
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
  
        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher, GeneralSettings settings):base(commandDispatcher)
        {
            _userService = userService;           
        }
        public async Task<IActionResult> Get()
        {
            var users = await _userService.BrowseAsync();
            return Json(users);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Json(user);
        }     


        [HttpPost("")]
        public async Task <IActionResult> Post([FromBody]CreateUser command)
        {
            await  CommandDispatcher.DispatcherAsync(command);           
            return Created($"users/{command.Email}", new object());
        }

     


    }
}
