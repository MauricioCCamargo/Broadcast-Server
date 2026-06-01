using System.Net;
using System.Net.Sockets;
using System.Text;

namespace src.Client
{
    public class Connect
    {
        public async Task ConnectClient(IPEndPoint ipEndPoint)
        {
            var clientID = Guid.NewGuid().ToString();

            using Socket client = new(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            await client.ConnectAsync(ipEndPoint);

            Console.WriteLine($"Socket client connected to address: {ipEndPoint}");

            _ = Task.Run(() => HandleSendMessage(client, clientID));

            while (true)
            {
                var buffer = new byte[1_024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                if (!string.IsNullOrWhiteSpace(response))
                {
                    Console.WriteLine($"Socket client ID: {clientID}  received message: \"{response}\"");
                }
            }
        }

        private async Task HandleSendMessage(Socket client, string clientID)
        {
            while (true)
            {
                var message = Console.ReadLine();

                if (message == null)
                    return;

                var messageBytes = Encoding.UTF8.GetBytes(message);
                _ = await client.SendAsync(messageBytes, SocketFlags.None);
                Console.WriteLine($"Socket client ID: {clientID} sent message: \"{message}\"");
            }
        }
    }
}
