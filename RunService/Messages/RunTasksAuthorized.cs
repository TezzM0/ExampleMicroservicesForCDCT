using NServiceBus;

namespace RunService.Messages
{
    public class RunTasksAuthorized : IEvent
    {
        public string ClientCode { get; set; }
        public int NumberOfTasks { get; set; }
    }
}