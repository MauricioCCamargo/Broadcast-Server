using System.Net;
using System.Net.Sockets;
using System.Text;

namespace src.Server
{

    public class Create
    {
        public async Task CreateServer()
        {

            IPEndPoint ipEndPoint = new(IPAddress.Parse("127.0.0.1"), 11_000);

            using Socket listener = new(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            listener.Bind(ipEndPoint);
            listener.Listen(100);

            Console.WriteLine($"Socket server created on address: {ipEndPoint}");

            while (true)
            {

                var handler = await listener.AcceptAsync();

                _ = HandleClient(handler);
            }
        }

        private async Task HandleClient(Socket client)
        {
            if (client != null)
                ConnectedClients.Clients.Add(client);
            else
                return;

            while (true)
            {
                var buffer = new byte[1_024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                if (!string.IsNullOrWhiteSpace(response))
                {
                    Console.WriteLine($"Socket server received message from a client: \"{response}\"");

                    await BroadcastMessageToClients(response, client);
                }
            }
        }

        private async Task BroadcastMessageToClients(string message, Socket sender)
        {
            if (ConnectedClients.Clients.Count == 0)
            {
                Console.WriteLine("No clients connected");
                return;
            }

            foreach (Socket client in ConnectedClients.Clients)
            {
                if (client == sender)
                    continue;

                var messageBytes = Encoding.UTF8.GetBytes(message);
                _ = await client.SendAsync(messageBytes, SocketFlags.None);
            }

            Console.WriteLine($"Socket server broadcasted message to clients: \"{message}\"");
        }
    }

}