using System.Net;

if (args.Length == 0)
    throw new Exception("No initialization arguments provided");

var mode = args[0].ToLowerInvariant();
var ipAddress = args.Length > 1 ? IPAddress.Parse(args[1]) : IPAddress.Parse("127.0.0.1");
var port = args.Length > 2 ? int.Parse(args[2]) : 11_000;
var ipEndPoint = new IPEndPoint(ipAddress, port);

if (mode == "start")
    await new src.Server.Create().CreateServer(ipEndPoint);
else if (mode == "connect")
    await new src.Client.Connect().ConnectClient(ipEndPoint);
else
    throw new Exception($"Unknown initialization argument: {args[0]}");
