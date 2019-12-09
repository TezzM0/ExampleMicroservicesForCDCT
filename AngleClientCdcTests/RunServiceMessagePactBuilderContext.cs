using CdcTestsShared;

namespace AngleClientCdcTests
{
    public class RunServiceMessagePactBuilderContext : MessagePactBuilderContext
    {
        public RunServiceMessagePactBuilderContext() 
            : base("AngleClient", "RunService")
        {
        }
    }
}
