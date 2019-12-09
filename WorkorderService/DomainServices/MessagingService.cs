using System;
using System.Threading.Tasks;
using NServiceBus;
using WorkorderService.Messages;

namespace WorkorderService.DomainServices
{
    public sealed class MessagingConfiguration : IDisposable
    {
        public IEndpointInstance EndpointInstance { get; private set; }

        private async Task<IMessageSession> GetMessageSession()
        {
            if (EndpointInstance == null)
            {
                var endpointConfiguration = new EndpointConfiguration("WorkorderService");
                var transport = endpointConfiguration.UseTransport<LearningTransport>();
                transport.StorageDirectory("C:/Users/Terry.Rossow/source/repos/MessageQueue");
                EndpointInstance = await Endpoint.Start(endpointConfiguration)
                    .ConfigureAwait(false);
            }

            return EndpointInstance;
        }

        public void Dispose()
        {
            if (EndpointInstance != null)
            {
                EndpointInstance.Stop()
                    .ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        public void Initialize()
        {
            GetMessageSession()
                .ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
