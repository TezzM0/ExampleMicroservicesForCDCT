using System;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using RunService.Messages;
using WorkorderService.Messages;

namespace RunService.Handlers
{
    public class WorkorderCommittedHandler : IHandleMessages<WorkorderCommitted>
    {
        private static Random random = new Random();

        public async Task Handle(WorkorderCommitted message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Workorder {message.WorkorderId} committed!");

            Thread.Sleep(3000);
            await context.Publish(new RunTasksAuthorized
            {
                ClientCode = message.ClientCode,
                NumberOfTasks = random.Next()
            });
        }
    }
}
