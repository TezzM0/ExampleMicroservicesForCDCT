using NServiceBus;
using System;
using System.Threading.Tasks;
using WorkorderService.Messages;

namespace AngleClient.Handlers
{
    public class WorkorderCommittedHandler : IHandleMessages<WorkorderCommitted>
    {
        public Task Handle(WorkorderCommitted message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Workorder created for client {message.ClientCode} (Workorder ID is: {message.WorkorderId}");
            return Task.CompletedTask;
        }
    }
}
