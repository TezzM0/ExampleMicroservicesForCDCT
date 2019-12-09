namespace CdcTestsShared
{
    public class ProviderStateHost<TProviderStateMiddleware> 
        : WebApplicationHost<ProviderStateStartup<TProviderStateMiddleware>>
    {
        public ProviderStateHost()
        : base("http://localhost:9001")
        {
        }
    }
}
