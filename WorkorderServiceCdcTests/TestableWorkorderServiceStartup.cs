using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using WorkorderService;
using WorkorderService.DomainServices;

namespace WorkorderServiceCdcTests
{
    public class TestableWorkorderServiceStartup : WorkorderService.Startup
    {
        public TestableWorkorderServiceStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(typeof(Startup).Assembly));

            RemoveExistingClientService(services);
            AddMockClientService(services);
        }

        private void AddMockClientService(IServiceCollection services)
        {
            services.AddTransient(x =>
                WorkorderServiceConsumedByAngleClientProviderStateMiddleware.MockClientService);
        }

        private void RemoveExistingClientService(IServiceCollection services)
        {
            var clientService = services.SingleOrDefault(d => d.ServiceType == typeof(IClientService));
            if (clientService != null)
            {
                services.Remove(clientService);
            }
        }
    }
}
