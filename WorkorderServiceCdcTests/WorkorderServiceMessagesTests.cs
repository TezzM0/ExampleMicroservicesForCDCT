using CdcTestsShared;
using WorkorderServiceCdcTests.MessageProviderStates;
using Xunit;

namespace WorkorderServiceCdcTests
{
    public class WorkorderServiceMessagesTests : IClassFixture<PactEnvironment>
    {
        private readonly PactEnvironment _pactEnvironment;

        public WorkorderServiceMessagesTests(PactEnvironment pactEnvironment)
        {
            _pactEnvironment = pactEnvironment;
        }

        [Fact]
        public void WorkorderServiceMessagesHonoursPactWithAngleClientMessages()
        {
            var config = new PactNetMessages.PactVerifierConfig
            {
                PublishVerificationResults = _pactEnvironment.PublishVerificationResults,
                ProviderVersion = _pactEnvironment.ProviderVersion
            };

            using var pactVerifier = new PactNetMessages.PactVerifier(() => { }, () => { }, config);
            pactVerifier.SetUpProviderStates(
                new AWorkorderHasBeenCreatedForAClient("BOB"));

            var consumerName = "AngleClient.Messages";
            var providerName = "WorkorderService.Messages";
            pactVerifier.VerifyUsingPactBroker(providerName, consumerName, _pactEnvironment);
        }

        [Fact]
        public void WorkorderServiceMessagesHonoursPactWithRunServiceMessages()
        {
            var config = new PactNetMessages.PactVerifierConfig
            {
                PublishVerificationResults = _pactEnvironment.PublishVerificationResults,
                ProviderVersion = _pactEnvironment.ProviderVersion
            };

            using var pactVerifier = new PactNetMessages.PactVerifier(() => { }, () => { }, config);
            pactVerifier.SetUpProviderStates(
                new AWorkorderHasBeenCreatedForAClient("ALICE"));

            var consumerName = "RunService.Messages";
            var providerName = "WorkorderService.Messages";
            pactVerifier.VerifyUsingPactBroker(providerName, consumerName, _pactEnvironment);
        }
    }
}
