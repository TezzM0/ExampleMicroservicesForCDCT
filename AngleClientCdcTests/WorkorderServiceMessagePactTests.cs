﻿using WorkorderService.Messages;
using Xunit;

namespace AngleClientCdcTests
{
    public class WorkorderServiceMessagePactTests : IClassFixture<WorkorderServiceMessagePactBuilderContext>
    {
        private readonly WorkorderServiceMessagePactBuilderContext _pactBuilderContext;

        public WorkorderServiceMessagePactTests(WorkorderServiceMessagePactBuilderContext pactBuilderContext)
        {
            _pactBuilderContext = pactBuilderContext;
        }

        [Fact]
        public void WorkorderCommittedMessageReceivedAfterWorkorderIsCreated()
        {
            _pactBuilderContext.PactMessageBuilder.MockMq()
                .Given("A workorder has been created for the client BOB")
                .UponReceiving("A workorder committed message")
                .WithMetaData(new { })
                .WithContent(new WorkorderCommitted
                {
                    ClientCode = "BOB"
                });
        }
    }
}
