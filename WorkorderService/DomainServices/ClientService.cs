using System;
using System.Linq;

namespace WorkorderService.DomainServices
{
    public class ClientService
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

        public static bool DoesClientExist(string clientCode)
        {
            return clientCodes.Contains(clientCode);
        }
    }
}
