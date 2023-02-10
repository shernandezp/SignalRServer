namespace SignalRServer.Controllers
{
    using Microsoft.AspNetCore.SignalR;
    using SignalRServer.Handler;

    public class MyHub : Hub
    {
        //private readonly IRepository repository;
        private readonly ILogger<MyHub> logger;
        private readonly IClientHandler clientHandler;
        private readonly IEnumerable<HubUser> users;

        public MyHub(
            //IRepository repository,
            ILogger<MyHub> logger,
            IClientHandler clientHandler)
        {
            //this.repository = repository;
            users = clientHandler.Users;
            this.clientHandler = clientHandler;
            this.logger = logger;
        }

        public void OnConnected(string userId)
        {
            try
            {
                if (!users.Any(w => w.UserId.Equals(userId)))
                {
                    var user = new HubUser
                    {
                        UserId = userId,
                        ConnectionId = Context.ConnectionId
                    };
                    clientHandler.AddUser(user);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                if (users.Any(x => x.ConnectionId == Context.ConnectionId))
                {
                    clientHandler.RemoveUser(Context.ConnectionId);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            await base.OnDisconnectedAsync(exception);
        }

        /*public async Task SaveMessage(string message)
        {
            try
            {
                await repository.SaveMessage(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
        }*/
    }
}