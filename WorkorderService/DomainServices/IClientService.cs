namespace WorkorderService.DomainServices
{
    public interface IClientService
    {
        bool DoesClientExist(string clientCode);
    }
}