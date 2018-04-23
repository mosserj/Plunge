using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Xunit;
using System.Threading.Tasks;

namespace backend.Integration.Test
{
    /// <summary>
    /// basic integration test so I can explore setting up a CI environment
    /// </summary>
    public class ProductAPIShould : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public ProductAPIShould(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ReturnHelloWorld()
        {
            // Act
            var response = await _fixture.Client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("[\"value1\",\"value2\"]",
                responseString);
        }
    }
}
