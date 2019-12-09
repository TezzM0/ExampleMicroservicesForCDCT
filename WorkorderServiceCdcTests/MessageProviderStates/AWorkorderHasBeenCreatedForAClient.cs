using AutoFixture;
using CdcTestsShared;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NServiceBus;
using NServiceBus.Testing;
using PactNetMessages.Mocks.MockMq.Models;
using System.Linq;
using NSubstitute;
using WorkorderService.Controllers;
using WorkorderService.DomainServices;

namespace WorkorderServiceCdcTests.MessageProviderStates
{
    public class AWorkorderHasBeenCreatedForAClient : IMessageProviderState
    {
        private readonly string _clientCode;
        private readonly Fixture _fixture;
        private readonly TestableMessageSession _messageSession;

        public AWorkorderHasBeenCreatedForAClient(string clientCode)
        {
            _clientCode = clientCode;
            _fixture = AutoFixtureProvider.CreateAutoFixture();
            _messageSession = new TestableMessageSession();
            _fixture.Register<IMessageSession>(() => _messageSession);   
        }

        public string Name => $"A workorder has been created for the client {_clientCode}";

        public Message SetUp()
        {
            _fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());
            var clientService = _fixture.Freeze<IClientService>();
            clientService.DoesClientExist(Arg.Is(_clientCode)).Returns(true);
            var workorderController = _fixture.Create<WorkorderController>();

            workorderController.CreateWorkorder(_clientCode)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            return new Message
            {
                Contents = _messageSession.PublishedMessages.Single().Message,
                MetaData = _messageSession.PublishedMessages.Single().Options.GetHeaders(),
                ProviderState = Name
            };
        }

        public void TearDown()
        {
        }
    }
}