using System.Net;
using System.Net.Sockets;

namespace GDNetworking.utils;

public abstract class AbstractServer
{
    private int port;
    private TcpListener server = null;

    public AbstractServer(int port)
    {
        this.port = port;
    }

    public void Start()
    {
        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Server started on port: " + port);
            while (true)
            {
                Console.WriteLine("Waiting for clients ...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected ...");
                ProcessRequest(client);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Starting server error: " + e.Message);
            Stop();
        }
    }

    protected abstract void ProcessRequest(TcpClient client);

    public void Stop()
    {
        try
        {
            server.Stop();
            Console.WriteLine("Server stopped.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Stopping server error: " + e.Message);
        }
    }
}