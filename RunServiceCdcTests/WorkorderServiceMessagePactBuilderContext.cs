using CdcTestsShared;

namespace RunServiceCdcTests
{
    public class WorkorderServiceMessagePactBuilderContext : MessagePactBuilderContext
    {
        public WorkorderServiceMessagePactBuilderContext() 
            : base("RunService", "WorkorderService")
        {
        }
    }
}
