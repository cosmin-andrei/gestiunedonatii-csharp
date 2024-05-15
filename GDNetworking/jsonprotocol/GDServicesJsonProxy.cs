using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using GDServices;
using System.Text;
using GestiuneDonatii.model;
using Newtonsoft.Json;

namespace GDNetworking.jsonprotocol;

public class GDServicesJsonProxy : IServices
{
    private string host;
    private int port;
    private IObserver client;

    private StreamReader input;
    private StreamWriter output;
    private TcpClient connection;

    private BlockingCollection<Response> qresponses;
    private volatile bool finished;

    public GDServicesJsonProxy(string host, int port)
    {
        this.host = host;
        this.port = port;
        qresponses = new BlockingCollection<Response>();
        Console.WriteLine("GDServicesJsonProxy");
    }

    public void Login(Voluntar user, IObserver client)
    {
        InitializeConnection();

        var req = JsonProtocolUtils.CreateLoginRequest(user);
        SendRequest(req);
        var response = ReadResponse();
        if (response.Type == ResponseType.OK)
        {
            this.client = client;
            Console.WriteLine(client);
            return;
        }

        if (response.Type == ResponseType.ERROR)
        {
            var err = response.ErrorMessage;
            CloseConnection();
            throw new ServiceException(err);
        }
    }

    public void Logout(Voluntar user, IObserver client)
    {
        var req = JsonProtocolUtils.CreateLogoutRequest(user);
        SendRequest(req);
        var response = ReadResponse();
        CloseConnection();
        if (response.Type == ResponseType.ERROR)
        {
            var err = response.ErrorMessage;
            throw new ServiceException(err);
        }
    }

    private void CloseConnection()
    {
        finished = true;
        try
        {
            if (input != null)
                input.Close();
            output.Close();
            connection.Close();
            client = null;
        }
        catch (IOException e)
        {
            Console.WriteLine("Error closing connection: " + e.Message);
        }
    }

    private void SendRequest(Request request)
    {
        var reqLine = JsonConvert.SerializeObject(request);
        try
        {
            output.WriteLine(reqLine);
            output.Flush();
        }
        catch (Exception e)
        {
            throw new ServiceException("Error sending object " + e.Message);
        }
    }

    private Response ReadResponse()
    {
        try
        {
            return qresponses.Take();
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    private void InitializeConnection()
    {
        try
        {
            TcpClient tcpClient = new TcpClient(host, port);
            NetworkStream networkStream = tcpClient.GetStream();
            output = new StreamWriter(networkStream, Encoding.UTF8);
            input = new StreamReader(networkStream, Encoding.UTF8);
            finished = false;
            StartReader();
            Console.WriteLine("Connection initialized");
        }
        catch (IOException e)
        {
            Console.WriteLine("Error initializing connection: " + e.Message);
        }
    }

    private void StartReader()
    {
        var thread = new Thread(new ThreadStart(ReaderThread));
        thread.Start();
    }

    private void HandleUpdate(Response response)
    {
        if (response.Type == ResponseType.NEW_DONATOR)
        {
            var donator = response.Donator;
            Console.WriteLine("Donator nou " + donator);
            try
            {
                client.NewDonator(donator);
            }
            catch (ServiceException e)
            {
                Console.WriteLine("Error handling new donator: " + e.Message);
            }
        }

        if (response.Type == ResponseType.NEW_DONATION)
        {
            var donatie = response.Donatie;
            Console.WriteLine("Donatie noua " + donatie);
            try
            {
                client.NewDonation(donatie);
            }
            catch (ServiceException e)
            {
                Console.WriteLine("Error handling new donation: " + e.Message);
            }
        }
    }

    private bool IsUpdate(Response response)
    {
        return response != null &&
               (response.Type == ResponseType.NEW_DONATION || response.Type == ResponseType.NEW_DONATOR);
    }

    public void AddDonator(Donator donator)
    {
        var req = JsonProtocolUtils.CreateAddDonatorRequest(donator);
        Console.WriteLine("Sending request: " + req);
        SendRequest(req);
        var response = ReadResponse();
        if (response.Type == ResponseType.ERROR)
        {
            var err = response.ErrorMessage;
            throw new ServiceException(err);
        }
    }

    public List<Donator> GetDonatori()
    {
        var req = JsonProtocolUtils.CreateGetDonatoriRequest();
        SendRequest(req);
        var response = ReadResponse();
        if (response.Type == ResponseType.ERROR)
        {
            var err = response.ErrorMessage;
            throw new ServiceException(err);
        }

        return response.Donatori;
    }

    public Dictionary<string, float> GetAllDonatii()
    {
        var req = JsonProtocolUtils.CreateDonatiiRequest();
        SendRequest(req);
        var response = ReadResponse();
        if (response.Type == ResponseType.ERROR)
        {
            var err = response.ErrorMessage;
            throw new ServiceException(err);
        }

        return response.Donatii;
    }

    public Donator FindDonator(Donator donator)
    {
        var req = JsonProtocolUtils.CreateFindDonatorRequest(donator);
        SendRequest(req);
        var response = ReadResponse();
        if (response.Type == ResponseType.ERROR)
        {
            var err = response.ErrorMessage;
            throw new ServiceException(err);
        }

        return response.Donator;
    }

    public void AddDonatie(Donatie donatie)
    {
        var req = JsonProtocolUtils.CreateNewDonationRequest(donatie);
        SendRequest(req);
        var response = ReadResponse();
        if (response.Type == ResponseType.ERROR)
        {
            var err = response.ErrorMessage;
            throw new ServiceException(err);
        }
    }

    public Cauza FindByNume(Cauza cauza)
    {
        var req = JsonProtocolUtils.CreateFindCauzaRequest(cauza);
        SendRequest(req);
        var response = ReadResponse();
        if (response.Type == ResponseType.ERROR)
        {
            var err = response.ErrorMessage;
            throw new ServiceException(err);
        }

        return response.Cauza;
    }

    private void ReaderThread()
    {
        Console.WriteLine("Reader thread started");
        
        while (!finished)
        {
            try
            {
                var responseLine = input.ReadLine();
                Console.WriteLine("response received " + responseLine);
                var response = JsonConvert.DeserializeObject<Response>(responseLine);
                if (IsUpdate(response))
                {
                    HandleUpdate(response);
                }
                else
                {
                    try
                    {
                        qresponses.Add(response);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error adding response to queue: " + e.Message);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Reading error: " + e.Message);
                Console.WriteLine("Stack trace: " + e.StackTrace);
            }
        }
    }
}