using PactNetMessages.Mocks.MockMq.Models;

namespace CdcTestsShared
{
    public interface IMessageProviderState
    {
        string Name { get; }

        Message SetUp();

        void TearDown();
    }
}