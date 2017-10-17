using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Passenger.Api;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Passenger.Infrastructure.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.TestsEndToEnd.Controller
{
    [TestClass]
    public class UsersControllerTests : ControllerTestsBase
    {
   
        [TestMethod]      
        public async Task given_valid_email_user_should_exist()
        {
            var email = "user1@test.com";
            var response = await Client.GetAsync($"users/{email}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var user = await GetUserAsync(email);
            Xunit.Assert.Equal(user.Email, email);   
                                      
        }
        [TestMethod]
        public async Task Given__invalid_email_user_should_not_exist()
        {
            var email = "empty@gmail.com";
            var response = await Client.GetAsync($"users/{email}");
            Xunit.Assert.Equal(response.StatusCode, HttpStatusCode.NotFound);           
        }      

        [TestMethod]
        public async Task given_unique_emial_user_should_be_created()
        {
            var email = "xardas999@gmail.com";
            var request = new CreateUser
            {
                Email = email,
                UserName = "Test",
                Password = "secret"
            };
            var payload = GetPayLoad(request);
            var response = await Client.PostAsync("users", payload);
            Xunit.Assert.Equal(response.StatusCode, HttpStatusCode.Created);
            Xunit.Assert.Equal(response.Headers.Location.ToString(), $"users/{request.Email}");

            var user = await GetUserAsync(request.Email);
            Xunit.Assert.Equal(user.Email, request.Email);
        }      

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }

    }
}
    