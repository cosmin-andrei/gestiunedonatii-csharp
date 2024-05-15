using System.Net.Sockets;
using System.Transactions;
using GDNetworking.jsonprotocol;
using GDServices;
using Protobuf;


namespace GDNetworking.utils;

public class GDProtoConcurrentServer: AbsConcurrentServer
{

    private IServices server;
    
    public GDProtoConcurrentServer(int port, IServices server) : base(port)
    {
        this.server = server;
        Console.WriteLine("Chat- ChatProtoConcurrentServer");
    }

    protected override Thread CreateWorker(TcpClient client)
    {
        ProtoGDWorker worker = new ProtoGDWorker(server, client);
        Thread workerThread = new Thread(worker.Run);
        return workerThread;
    }
}