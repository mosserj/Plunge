using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace backend.Integration.Test
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(GetContentRootPath())
                .UseEnvironment("Development")
                .UseStartup<backend.Startup>()
                .UseApplicationInsights();

            _testServer = new TestServer(builder);

            Client = _testServer.CreateClient();
        }

        private string GetContentRootPath()
        {
            string testProjectPath = PlatformServices.Default.Application.ApplicationBasePath;

            var relativePathToWebProject = @"..\..\..\..\backend";

            return Path.Combine(testProjectPath, relativePathToWebProject);
        }
        
        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}
