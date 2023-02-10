namespace SignalRServer.Handler
{
    public interface IClientHandler
    {
        List<HubUser> Users { get; set; }
        void AddUser(HubUser user);
        void RemoveUser(string connectionId);
    }
}