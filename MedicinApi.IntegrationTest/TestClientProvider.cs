using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace MedicinApi.IntegrationTest
{
    public class TestClientProvider : IDisposable
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }
        public TestClientProvider()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            Client = _server.CreateClient();
        }

        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();
        }
    }
}
