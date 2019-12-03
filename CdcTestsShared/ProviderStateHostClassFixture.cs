namespace CdcTestsShared
{
    public class ProviderStateHostClassFixture<TProviderStateMiddleware> 
        : WebApplicationHostClassFixture<ProviderStateStartup<TProviderStateMiddleware>>
    {
        public ProviderStateHostClassFixture()
        : base("http://localhost:9001")
        {
        }
    }
}
