using System;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;

namespace AngleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var baseUri = args.Length > 0 ? args[0] : "http://localhost";
            var workorderServiceClient = new WorkorderServiceClient(baseUri);

            var endpointConfiguration = new EndpointConfiguration("AngleClient");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.StorageDirectory("C:/Users/Terry.Rossow/source/repos/MessageQueue");
            endpointConfiguration.AutoSubscribe();
            var endpointInstance = await Endpoint.Start(endpointConfiguration);

            Console.WriteLine("Welcome to Angle! Press Enter to create a workorder! Press esc to quit.");
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var keyPressed = Console.ReadKey();
                    if (keyPressed.Key == ConsoleKey.Enter)
                    {
                        var clientCode = Clients.GetClientCode();
                        Console.WriteLine($"Requesting workorder for {clientCode}...");
                        await workorderServiceClient.CreateWorkorder(clientCode);
                    }
                    else if (keyPressed.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

                Thread.Sleep(100);
            }

            await endpointInstance.Stop();
        }
    }
}
