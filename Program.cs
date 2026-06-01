if (args.Length == 0)
    throw new Exception("No initialization arguments provided");

if (args.Contains("start"))
    await new src.Server.Create().CreateServer();
else if (args.Contains("connect"))
    await new src.Client.Connect().ConnectClient();



