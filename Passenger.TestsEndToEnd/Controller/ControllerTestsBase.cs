using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Passenger.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Passenger.TestsEndToEnd.Controller
{
    public abstract class ControllerTestsBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        public ControllerTestsBase()
        {
            Server = new TestServer(new WebHostBuilder()
                                    .UseStartup<Startup>());
            Client = Server.CreateClient();
        }
        protected StringContent GetPayLoad(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

    }
}
