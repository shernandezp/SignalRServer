namespace SignalRServer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using SignalRServer.Handler;

    [ApiController]
    public class ClientController : ControllerBase
    {
        //private readonly IRepository repository;
        private readonly ILogger<ClientController> logger;
        private readonly IHubContext<MyHub> hubContext;
        private readonly IClientHandler clientHandler;

        public ClientController(
            //IRepository repository, 
            IHubContext<MyHub> hubContext,
            IClientHandler clientHandler,
            ILogger<ClientController> logger)
        {
            //this.repository = repository;
            this.hubContext = hubContext;
            this.clientHandler = clientHandler;
            this.logger = logger;
        }

        [HttpPost]
        [Route("api/Alarm/AddMobile")]
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
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
