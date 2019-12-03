using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CdcTestsShared
{
    public abstract class WebApplicationHostClassFixture<TStartup> : IDisposable 
        where TStartup : class
    {
        private readonly IWebHost _webHost;

        protected WebApplicationHostClassFixture(string baseUri)
        {
            BaseUri = baseUri;

            _webHost = WebHost.CreateDefaultBuilder()
                .UseUrls(BaseUri)
                .UseStartup<TStartup>()
                .Build();

            _webHost.Start();
        }

        public string BaseUri { get; }

        private bool _disposed = false;

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _webHost.StopAsync().GetAwaiter().GetResult();
            _webHost.Dispose();
            _disposed = true;
        }
    }
}
