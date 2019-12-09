using NServiceBus;
using RunService.Messages;
using System;
using System.Threading.Tasks;

namespace AngleClient.Handlers
{
    public class RunTasksAuthorizedHandler : IHandleMessages<RunTasksAuthorized>
    {
        public Task Handle(RunTasksAuthorized message, IMessageHandlerContext context)
        {
            Console.WriteLine($"{message.NumberOfTasks} tasks authorized for workorder: {message.WorkorderId}");
            return Task.CompletedTask;
        }
    }
}
