using System;
using PactNetMessages;

namespace CdcTestsShared
{
    public class MessagePactClassFixture : IDisposable
    {
        public MessagePactClassFixture(string consumerName, string providerName)
        {
            var pactConfig = new PactConfig
            {
                PactDir = @".\pacts",
                LogDir = @".\pact_logs"
            };

            PactMessageBuilder = new PactMessageBuilder(pactConfig);

            PactMessageBuilder.ServiceConsumer(consumerName + ".Messages").HasPactWith(providerName + ".Messages");
        }

        public IPactMessageBuilder PactMessageBuilder { get; set; }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            PactMessageBuilder.Build();
            _disposed = false;
        }
    }
}
