using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreTodo.IntegrationTests
{
    public class TestFixure : IDisposable
    {
        private readonly TestServer testServer;

        public HttpClient Client { get; }

        public TestFixure()
        {
            var builder = new WebHostBuilder()
                .UseStartup<AspNetCoreTodo.Startup>().ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"../../../../AspNetCoreTodo"));

                    config.AddJsonFile("appsettings.json");
                });

            testServer = new TestServer(builder);

            Client = testServer.CreateClient();
            Client.BaseAddress = new Uri(@"http:/localhost:8888");
        }

        public void Dispose()
        {
            Client.Dispose();
            testServer.Dispose();
        }
    }
}
