using AngleClient;
using FluentAssertions;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using PactNet.Matchers;
using Xunit;

namespace AngleClientCdcTests
{
    public class WorkorderServicePactTests : IClassFixture<WorkorderServicePactBuilderContext>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly WorkorderServiceClient _mockWorkorderServiceClient;

        public WorkorderServicePactTests(WorkorderServicePactBuilderContext pactBuilderContext)
        {
            _mockProviderService = pactBuilderContext.MockProviderService;
            _mockProviderService.ClearInteractions();
            _mockWorkorderServiceClient = new WorkorderServiceClient(pactBuilderContext.MockProviderServiceBaseUri);
        }

        [Fact]
        public void CreateWorkorderHandlesInvalidClientCode()
        {
            _mockProviderService
                .Given("A client with code DERP does not exist")
                .UponReceiving("A request to create a workorder with the client code DERP")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Post,
                    Path = "/Workorder",
                    Query ="ClientCode=DERP"
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 400,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "text/plain; charset=utf-8" }
                    },
                    Body = "Client code does not exist"
                });

            _mockWorkorderServiceClient
                .Invoking(x => x.CreateWorkorder("DERP")
                    .ConfigureAwait(false).GetAwaiter().GetResult())
                .Should()
                .Throw<Exception>()
                .WithMessage(
                    "Client code does not exist", 
                    "workorder cannot be created for a client that doesn't exist");
        }

        [Fact]
        public void CreateWorkorderAcceptsValidClientCode()
        {
            _mockProviderService
                .Given("A client with code COOL exists")
                .UponReceiving("A request to create a workorder with a client code COOL")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Post,
                    Path = "/Workorder",
                    Query = "ClientCode=COOL"
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new 
                    {
                        Id = Match.Regex(
                            "600b6ad4-1705-4c76-8874-1ed62d155b2d",
                            @"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")
                    }
                });

            var actualWorkorderCreated = _mockWorkorderServiceClient.CreateWorkorder("COOL")
                .ConfigureAwait(false).GetAwaiter().GetResult();

            Guid dummy;
            actualWorkorderCreated.Id.Should().Match(
                id => Guid.TryParse((string)id, out dummy), 
                "response should contain a valid GUID");
        }
    }
}
