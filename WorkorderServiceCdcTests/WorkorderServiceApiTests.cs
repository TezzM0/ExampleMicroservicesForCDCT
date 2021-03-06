using CdcTestsShared;
using PactNet;
using PactNet.Infrastructure.Outputters;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace WorkorderServiceCdcTests
{
    public class WorkorderServiceApiTests 
        : IClassFixture<PactEnvironment>, 
          IClassFixture<ProviderStateHost<WorkorderServiceConsumedByAngleClientProviderStateMiddleware>>, 
          IClassFixture<ProviderHost<TestableWorkorderServiceStartup>>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly string _pactServiceUri;
        private readonly string _workorderServiceUri;
        private readonly PactEnvironment _pactEnvironment;

        public WorkorderServiceApiTests(
            PactEnvironment pactEnvironment,
            ProviderStateHost<WorkorderServiceConsumedByAngleClientProviderStateMiddleware> pactHostFixture,
            ProviderHost<TestableWorkorderServiceStartup> subjectWebApplication,
            ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _pactServiceUri = pactHostFixture.BaseUri;
            _workorderServiceUri = subjectWebApplication.BaseUri;
            _pactEnvironment = pactEnvironment;
        }

        [Fact]
        public void WorkorderServiceHonoursPactWithAngleClient()
        {
            var config = new PactVerifierConfig
            {
                PublishVerificationResults = _pactEnvironment.PublishVerificationResults,
                ProviderVersion = _pactEnvironment.ProviderVersion,

                // NOTE: We default to using a ConsoleOutput,
                // however xUnit 2 does not capture the console output,
                // so a custom outputter is required.
                Outputters = new List<IOutput> { new XUnitOutput(_outputHelper) },

                // Output verbose verification logs to the test output
                Verbose = true
            };

            var consumerName = "AngleClient";
            var providerName = "WorkorderService";
            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier.ProviderState($"{_pactServiceUri}/provider-states")
                .ServiceProvider(providerName, _workorderServiceUri)
                .HonoursPactWith(consumerName)
                .PactUri(
                    $"{_pactEnvironment.PactBrokerBaseUri}/pacts/provider/{providerName}/consumer/{consumerName}/latest",
                    new PactUriOptions(_pactEnvironment.PactBrokerAuthenticationToken))
                .Verify();
        }
    }
}
