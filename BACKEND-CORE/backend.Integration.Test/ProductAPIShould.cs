using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Xunit;
using System.Threading.Tasks;

namespace backend.Integration.Test
{
    public class ProductAPIShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ProductAPIShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseContentRoot(@"F:\dev\security\jwt\BACKEND-CORE\backend")
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ReturnHelloWorld()
        {
            // Act
            var response = await _client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("[\"value1\",\"value2\"]",
                responseString);
        }
    }
}
