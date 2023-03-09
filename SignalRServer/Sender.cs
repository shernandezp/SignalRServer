namespace SignalRServer
{
    using Microsoft.AspNetCore.SignalR;
    using SignalRServer.Controllers;
    using SignalRServer.Handler;

    public class Sender : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IHubContext<MyHub> hubContext;
        private readonly ILogger<Sender> logger;
        private readonly IClientHandler clientHandler;
        public Sender(IServiceScopeFactory serviceScopeFactory,
            IHubContext<MyHub> hubContext,
            ILogger<Sender> logger,
            IClientHandler clientHandler)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.hubContext = hubContext;
            this.clientHandler = clientHandler;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(100000, cancellationToken);

                //var scope = serviceScopeFactory.CreateScope();
                //var repository = scope.ServiceProvider.GetRequiredService<IRepository>();

                foreach (var user in clientHandler.Users.ToList())
                {
                    //var messages = await repository.GetPendingMessages(user.UserId);
                    //await hubContext.Clients.Client(user.ConnectionId).SendAsync("broadcast", messages, cancellationToken);
                }
            }
        }
    }
}
