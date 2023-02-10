# SignalRServer

- SignalRService server with a Background Service to periodically update pending messages in its clients.
- It uses a singleton instance to persist in memory the list of connected users.
- I also include a controller to allow external actors to send individual messages to the Hub.
- The clients might need an unique id on their end (email?), so the flow of the Hub can work.

# Concepts

  - SignalR
  - Background Service
  - IHubContext
  - IServiceScopeFactory
  
# References
https://learn.microsoft.com/en-us/aspnet/core/signalr/background-services?view=aspnetcore-7.0
https://learn.microsoft.com/en-us/aspnet/signalr/overview/advanced/dependency-injection
