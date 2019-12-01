using NServiceBus;

namespace WorkorderService.Messages
{
    public class WorkorderCommitted : IEvent
    {
        public string WorkorderId { get; set; }
        public string ClientCode { get; set; }
    }
}