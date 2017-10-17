using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Passenger.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Passenger.Infrastructure.Commands.Users;
using System.Net;

namespace Passenger.TestsEndToEnd.Controller
{
    [TestClass]
    public class AccountControllerTests : ControllerTestsBase
    {
       
        [TestMethod]
        public async Task given_valid_current_and_new_password_it_should_be_changed()
        {
            var command = new ChangeUserPassword
            {
                CurrentPassword = "secret",
                NewPassword = "secret2"
            };
            var payload = GetPayLoad(command);
            var response = await Client.PutAsync("account/password", payload);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);

        }
    }
}
