using System.Linq;
using AutoFixture;
using CdcTestsShared;
using NServiceBus;
using NServiceBus.Testing;
using NSubstitute;
using PactNetMessages.Mocks.MockMq.Models;
using RunService.DomainServices;
using RunService.Handlers;
using WorkorderService.Messages;

namespace RunServiceCdcTests.MessageProviderStates
{
    public class AWorkorderHasBeenCommitted : IMessageProviderState
    {
        private readonly string _workorderId;
        private readonly int _numberOfRunTasks;
        private readonly Fixture _fixture;

        public AWorkorderHasBeenCommitted(string workorderId, int numberOfRunTasks)
        {
            _workorderId = workorderId;
            _numberOfRunTasks = numberOfRunTasks;
            _fixture = AutoFixtureProvider.CreateAutoFixture();
        }

        public string Name => $"Workorder with ID {_workorderId} and {_numberOfRunTasks} run tasks is committed";

        public Message SetUp()
        {
            var context = new TestableMessageHandlerContext();
            var runTasksProvider = _fixture.Freeze<IRunTasksProvider>();
            runTasksProvider.GetNumberOfTasks(Arg.Is(_workorderId)).Returns(_numberOfRunTasks);
            var message = _fixture.Build<WorkorderCommitted>().With(x => x.WorkorderId, _workorderId).Create();
            var workorderCommittedHandler = _fixture.Create<WorkorderCommittedHandler>();

            workorderCommittedHandler.Handle(message, context)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            return new Message
            {
                ProviderState = Name,
                Contents = context.PublishedMessages.Single().Message,
                MetaData = context.PublishedMessages.Single().Options.GetHeaders()
            };
        }

        public void TearDown()
        {
        }
    }
}
