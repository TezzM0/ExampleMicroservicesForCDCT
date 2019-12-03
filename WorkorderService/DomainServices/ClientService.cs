using System.Linq;

namespace WorkorderService.DomainServices
{
    public class ClientService : IClientService
    {
        private static string[] clientCodes = new[]
        {
            "BGO",
            "NUOL",
            "NTC",
            "CQT",
            "WYXP",
            "LXHA",
            "LOE",
            "MWWH",
            "SXZ"
        };

        public bool DoesClientExist(string clientCode)
        {
            return clientCodes.Contains(clientCode);
        }
    }
}
