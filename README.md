# Broadcast Server

Broadcast Server is a small .NET console project that shows how a TCP socket server can send messages between connected clients.

The project has two modes:

- `start` starts the socket server.
- `connect` starts a socket client.

By default, the server listens on `127.0.0.1:11000`. Each client connects to that server. When one client writes a message, the server receives it and sends it to the other connected clients.

You can also pass a custom IP address and port:

```bash
dotnet run -- start [ip] [port]
dotnet run -- connect [ip] [port]
```

## Requirements

- .NET SDK 10.0 or newer

## How to Run

Start the server in one terminal:

```bash
dotnet run -- start
```

Start a client in another terminal:

```bash
dotnet run -- connect
```

To use a custom address:

```bash
dotnet run -- start 127.0.0.1 12000
dotnet run -- connect 127.0.0.1 12000
```

To test broadcasting, open at least two client terminals. Type a message in one client and press Enter. The other clients should receive the message.

## Example

Terminal 1:

```bash
dotnet run -- start
```

Terminal 2:

```bash
dotnet run -- connect
```

Terminal 3:

```bash
dotnet run -- connect
```

Now type a message in Terminal 2. Terminal 3 should print that message.

## Notes

- This project is for learning and local testing.
- It listens on `127.0.0.1:11000` unless you pass another IP address and port.
- Messages are sent as UTF-8 text.
- The sender does not receive its own message back from the server.
