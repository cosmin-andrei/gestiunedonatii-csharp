using System.Net.Sockets;
using GDNetworking.jsonprotocol;
using GDServices;

namespace GDNetworking.utils;

public class GDJsonConcurrentServer : AbsConcurrentServer
{
    private IServices server;

    public GDJsonConcurrentServer(int port, IServices server) : base(port)
    {
        this.server = server;
        Console.WriteLine("Chat- ChatJsonConcurrentServer");
    }

    protected override Thread CreateWorker(TcpClient client)
    {
        GDClientJsonWorker worker = new GDClientJsonWorker(server, client);

        Thread workerThread = new Thread(worker.Run);
        return workerThread;
    }

}
