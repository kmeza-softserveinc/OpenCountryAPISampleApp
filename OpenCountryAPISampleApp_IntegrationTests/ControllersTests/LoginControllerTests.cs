using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace OpenCountryAPISampleApp_IntegrationTests.ControllersTests
{
    public class LoginControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;

        public LoginControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Login_Post_ReturnsOk_WithValidCredentials()
        {

            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost/")
            });

            var response = await client.PostAsync("/Login", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"email", "test1@email.com"},
                {"password", "password1"}
            }));

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

}
