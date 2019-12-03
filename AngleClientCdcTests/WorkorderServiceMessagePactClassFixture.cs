using System;
using System.Collections.Generic;
using System.Text;
using CdcTestsShared;

namespace AngleClientCdcTests
{
    public class WorkorderServiceMessagePactClassFixture : MessagePactClassFixture
    {
        public WorkorderServiceMessagePactClassFixture() 
            : base("AngleClient", "WorkorderService")
        {
        }
    }
}
