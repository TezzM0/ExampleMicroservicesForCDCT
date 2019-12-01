using System;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;

namespace RunService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var endpointConfiguration = new EndpointConfiguration("RunService");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.StorageDirectory("C:/Users/Terry.Rossow/source/repos/MessageQueue");
            endpointConfiguration.AutoSubscribe();
            var endpointInstance = await Endpoint.Start(endpointConfiguration);

            Console.WriteLine("Welcome to run service version 3.14! Press esc to stop servicing runs.");

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var keyPressed = Console.ReadKey();
                    if (keyPressed.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("No more runs....");
                        break;
                    }
                }

                Thread.Sleep(50);
            }

            await endpointInstance.Stop();
        }
    }
}
