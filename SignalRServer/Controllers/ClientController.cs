namespace SignalRServer.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.Handler;

[ApiController]
public class ClientController(
    //IRepository repository, 
    IHubContext<MyHub> hubContext,
    IClientHandler clientHandler,
    ILogger<ClientController> logger) : ControllerBase
{
    //private readonly IRepository repository;
    private readonly ILogger<ClientController> logger = logger;
    private readonly IHubContext<MyHub> hubContext = hubContext;
    private readonly IClientHandler clientHandler = clientHandler;

    [HttpPost]
    [Route("api/AddMessage")]
    public async Task<IActionResult> IndividualMessage(MessageDto message)
    {
        try
        {
            var user = clientHandler
                .Users
                .SingleOrDefault(x => x.UserId.Equals(message.UserId));

            if (user != null)
            {
                await hubContext.Clients.Client(user.ConnectionId).SendAsync("broadcast", message.Message);
            }
            else 
            {
                //repository.PersistMessage(message);
            }
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Message} - {StackTrace}", ex.Message, ex.StackTrace);
            return BadRequest(ex.Message);
        }
    }
}
