using NServiceBus;

namespace RunService.Messages
{
    public class RunTasksAuthorized : IEvent
    {
        public string WorkorderId { get; set; }
        public int NumberOfTasks { get; set; }
    }
}