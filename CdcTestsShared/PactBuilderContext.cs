using System;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace CdcTestsShared
{
    public abstract class PactBuilderContext : IDisposable
    {
        protected PactBuilderContext(string consumerName, string providerName)
        {
            // Using Spec version 2.0.0 more details at https://goo.gl/UrBSRc
            var pactConfig = new PactConfig
            {
                SpecificationVersion = "2.0.0",
                PactDir = @".\pacts",
                LogDir = @".\pact_logs"
            };

            PactBuilder = new PactNet.PactBuilder(pactConfig);
            PactBuilder.ServiceConsumer(consumerName).HasPactWith(providerName);

            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort => 9222;
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            PactBuilder.Build();
            _disposed = true;
        }
    }
}
