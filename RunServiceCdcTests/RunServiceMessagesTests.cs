using CdcTestsShared;
using RunServiceCdcTests.MessageProviderStates;
using Xunit;

namespace RunServiceCdcTests
{
    public class RunServiceMessagesTests : IClassFixture<PactEnvironment>
    {
        private readonly PactEnvironment _pactEnvironment;

        public RunServiceMessagesTests(PactEnvironment pactEnvironment)
        {
            _pactEnvironment = pactEnvironment;
        }

        [Fact]
        public void RunServiceMessagesHonoursPactWithAngleClientMessages()
        {
            var config = new PactNetMessages.PactVerifierConfig
            {
                PublishVerificationResults = _pactEnvironment.PublishVerificationResults,
                ProviderVersion = _pactEnvironment.ProviderVersion
            };

            using var pactVerifier = new PactNetMessages.PactVerifier(() => { }, () => { }, config);
            pactVerifier.SetUpProviderStates(
                new AWorkorderHasBeenCommitted("F24BBBDA-D703-407F-844F-DD94912E3BA4", 11));

            var consumerName = "AngleClient.Messages";
            var providerName = "RunService.Messages";
            pactVerifier.VerifyUsingPactBroker(providerName, consumerName, _pactEnvironment);
        }
    }
}
