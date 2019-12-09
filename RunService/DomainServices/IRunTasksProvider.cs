namespace RunService.DomainServices
{
    public interface IRunTasksProvider
    {
        int GetNumberOfTasks(string workorderId);
    }
}