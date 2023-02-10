namespace SignalRServer.Handler
{
    public class ClientHandler : IClientHandler
    {
        private List<HubUser> users;
        public List<HubUser> Users
        {
            get { return users; }
            set { users = value; }
        }

        public ClientHandler()
        {
            users = new List<HubUser>();
        }

        public void AddUser(HubUser user)
        {
            users.Add(user);
        }

        public void RemoveUser(string connectionId)
        {
            var connection = Users.FirstOrDefault(x => x.ConnectionId == connectionId);
            if (connection != null)
            {
                users.Remove(connection);
            }
        }
    }
}
