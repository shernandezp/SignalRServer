using SignalRServer;
using SignalRServer.Controllers;
using SignalRServer.Handler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddSingleton(typeof(IClientHandler), new ClientHandler());
//builder.Services.AddScoped<IRepository, Repository>();

builder.Services.Configure<HostOptions>(hostOptions =>
{
    hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});
builder.Services.AddHostedService<Sender>();

var app = builder.Build();
app.MapHub<MyHub>("/mobilemessageHub");

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
