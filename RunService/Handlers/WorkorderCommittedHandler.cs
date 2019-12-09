using System;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using RunService.DomainServices;
using RunService.Messages;
using WorkorderService.Messages;

namespace RunService.Handlers
{
    public class WorkorderCommittedHandler : IHandleMessages<WorkorderCommitted>
    {
        private readonly IRunTasksProvider _runTasksProvider;

        public WorkorderCommittedHandler(IRunTasksProvider runTasksProvider)
        {
            _runTasksProvider = runTasksProvider;
        }

        public async Task Handle(WorkorderCommitted message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Workorder {message.WorkorderId} committed!");

            Thread.Sleep(3000);
            await context.Publish(new RunTasksAuthorized
            {
                WorkorderId = message.WorkorderId,
                NumberOfTasks = _runTasksProvider.GetNumberOfTasks(message.WorkorderId)
            });
        }
    }
}
