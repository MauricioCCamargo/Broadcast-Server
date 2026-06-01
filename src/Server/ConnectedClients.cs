using System.Net.Sockets;

namespace src.Server
{
    public static class ConnectedClients
    {
        public static List<Socket> Clients { get; set; } = [];
    }
}