namespace SignalRServer.Controllers;

using Microsoft.AspNetCore.SignalR;
using SignalRServer.Handler;

public class MyHub(
    //IRepository repository,
    ILogger<MyHub> logger,
    IClientHandler clientHandler) : Hub
{
    private readonly IEnumerable<HubUser> users = clientHandler.Users;

    public void OnConnected(string userId)
    {
        ExceptionHandlers.HandleException(() =>
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
        }, logger);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        ExceptionHandlers.HandleException(() =>
        {
            if (users.Any(x => x.ConnectionId == Context.ConnectionId))
            {
                clientHandler.RemoveUser(Context.ConnectionId);
            }
        }, logger);
        await base.OnDisconnectedAsync(exception);
    }

    /*public async Task SaveMessage(string message)
    {
        await ExceptionHandlers.HandleExceptionAsync(async () =>
        {
            await repository.SaveMessage(message);
        }, logger);
    }*/
}