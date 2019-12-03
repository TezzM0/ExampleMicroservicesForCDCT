using Xunit;

namespace AngleClientCdcTests
{
    public class WorkorderServiceMessagePactTests : IClassFixture<WorkorderServiceMessagePactClassFixture>
    {
        private readonly WorkorderServiceMessagePactClassFixture _fixture;

        public WorkorderServiceMessagePactTests(WorkorderServiceMessagePactClassFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void WorkorderCommittedMessageReceivedAfterWorkorderIsCreated()
        {
            _fixture.PactMessageBuilder.MockMq()
                .Given("A workorder has been created for the client BOB")
                .UponReceiving("A workorder committed message")
                .WithMetaData(new { })
                .WithContent(new
                {
                    ClientCode = "BOB"
                });
        }
    }
}
