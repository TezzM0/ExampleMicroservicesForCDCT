namespace CdcTestsShared
{
    public class ProviderHost<TStartup> : WebApplicationHost<TStartup> 
        where TStartup : class
    {
        public ProviderHost() : base("http://localhost:9000")
        {
        }
    }
}
