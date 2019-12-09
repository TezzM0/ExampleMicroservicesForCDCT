namespace CdcTestsShared
{
    public class PactEnvironment
    {
        public string ProviderVersion { get; set; } =
            "1.0.1+1.Branch.master.Sha.e7f40378e856abc12ddc61dd52661be21384531b";
        public string PactBrokerBaseUri { get; set; } = "https://test_als_terry.pact.dius.com.au";
        public string PactBrokerAuthenticationToken { get; set; } = "XUxGLRjlW2wM-CBBFK0P9w";
        public bool PublishVerificationResults { get; set; } = true;
    }
}