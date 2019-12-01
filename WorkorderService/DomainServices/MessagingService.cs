using System;
using System.Threading.Tasks;
using NServiceBus;
using WorkorderService.Messages;

namespace WorkorderService.DomainServices
{
    public sealed class MessagingService : IDisposable
    {
        private IEndpointInstance _endpointInstance;

        public async Task Publish(WorkorderCommitted workorderCommitted)
        {
            var messageSession = await GetMessageSession()
                .ConfigureAwait(false);
            await messageSession.Publish(workorderCommitted)
                .ConfigureAwait(false);
        }

        private async Task<IMessageSession> GetMessageSession()
        {
            if (_endpointInstance == null)
            {
                var endpointConfiguration = new EndpointConfiguration("WorkorderService");
                var transport = endpointConfiguration.UseTransport<LearningTransport>();
                transport.StorageDirectory("C:/Users/Terry.Rossow/source/repos/MessageQueue");
                _endpointInstance = await Endpoint.Start(endpointConfiguration)
                    .ConfigureAwait(false);
            }

            return _endpointInstance;
        }

        public void Dispose()
        {
            if (_endpointInstance != null)
            {
                _endpointInstance.Stop()
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
