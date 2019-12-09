using System;

namespace RunService.DomainServices
{
    public class RunTasksProvider : IRunTasksProvider
    {
        private static Random _random = new Random();

        public int GetNumberOfTasks(string workorderId)
        {
            return _random.Next();
        }
    }
}
