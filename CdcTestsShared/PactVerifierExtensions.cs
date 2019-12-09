namespace CdcTestsShared
{
    public static class PactVerifierExtensions
    {
        public static void SetUpProviderStates(
            this PactNetMessages.PactVerifier pactVerifier,
            params IMessageProviderState[] providerStates)
        {
            foreach (var messageProviderState in providerStates)
            {
                pactVerifier.ProviderState(
                    messageProviderState.Name, messageProviderState.SetUp, messageProviderState.TearDown);
            }
        }

        public static void VerifyUsingPactBroker(
            this PactNetMessages.PactVerifier pactVerifier,
            string providerName,
            string consumerName,
            PactEnvironment pactEnvironment)
        {
            pactVerifier
                .MessageProvider(providerName)
                .HonoursPactWith(consumerName)
                .PactUri(
                    $"{pactEnvironment.PactBrokerBaseUri}/pacts/provider/{providerName}/consumer/{consumerName}/latest",
                    new PactNetMessages.PactUriOptions(pactEnvironment.PactBrokerAuthenticationToken))
                .Verify();
        }
    }
}
