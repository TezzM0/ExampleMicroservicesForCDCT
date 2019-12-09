using System;
using System.Collections.Generic;
using System.Text;
using CdcTestsShared;

namespace AngleClientCdcTests
{
    public class WorkorderServiceMessagePactBuilderContext : MessagePactBuilderContext
    {
        public WorkorderServiceMessagePactBuilderContext() 
            : base("AngleClient", "WorkorderService")
        {
        }
    }
}
