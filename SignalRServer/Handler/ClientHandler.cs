namespace SignalRServer.Handler;

public class ClientHandler : IClientHandler
{
    private List<HubUser> users;
    public List<HubUser> Users
    {
        get { return users; }
        set { users = value; }
    }

    public ClientHandler()
        => users = [];

    public void AddUser(HubUser user)
    {
        lock (users)
        {
            users.Add(user);
        }
    }

    public void RemoveUser(string connectionId)
    {
        lock (users)
        {
            var connection = users.FirstOrDefault(x => x.ConnectionId == connectionId);
            if (connection != null)
            {
                users.Remove(connection);
            }
        }
    }
}
