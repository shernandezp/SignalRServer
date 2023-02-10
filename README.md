# SignalRServer

- SignalRService server with a Background Service to periodically update pending messages in its clients.
- It uses a singleton instance to persist in memory the list of connected users.
- I also includes a controller to allow external actors send individual messages to the Hub.
- The clients migth need an unique id on their end (email?), so the flow of the Hub can work.

# Concepts

  - SignalR
  - Background Service
  - IHubContext
  - IServiceScopeFactory
