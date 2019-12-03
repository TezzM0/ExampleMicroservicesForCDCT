namespace CdcTestsShared
{
    public class ProviderHostClassFixture<TStartup> : WebApplicationHostClassFixture<TStartup> 
        where TStartup : class
    {
        public ProviderHostClassFixture() : base("http://localhost:9000")
        {
        }
    }
}
