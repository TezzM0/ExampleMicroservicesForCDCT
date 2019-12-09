using RunService.Messages;
using Xunit;

namespace AngleClientCdcTests
{
    public class RunServiceMessagePactTests : IClassFixture<RunServiceMessagePactBuilderContext>
    {
        private readonly RunServiceMessagePactBuilderContext _pactBuilderContext;

        public RunServiceMessagePactTests(RunServiceMessagePactBuilderContext pactBuilderContext)
        {
            _pactBuilderContext = pactBuilderContext;
        }

        [Fact]
        public void RunTasksAuthorizedMessageReceivedAfterWorkorderCommitted()
        {
            _pactBuilderContext.PactMessageBuilder.MockMq()
                .Given("Workorder with ID F24BBBDA-D703-407F-844F-DD94912E3BA4 and 11 run tasks is committed")
                .UponReceiving("A run tasks authorized message")
                .WithMetaData(new { })
                .WithContent(new RunTasksAuthorized
                {
                    WorkorderId = "F24BBBDA-D703-407F-844F-DD94912E3BA4",
                    NumberOfTasks = 11
                });
        }
    }
}