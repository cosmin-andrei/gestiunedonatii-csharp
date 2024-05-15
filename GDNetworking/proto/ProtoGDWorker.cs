using System.Net.Sockets;
using GD.Protocol;
using GDNetworking.jsonprotocol;
using GDServices;
using Google.Protobuf;

namespace Protobuf;

public class ProtoGDWorker : IObserver
{
    private IServices server;
    private TcpClient connection;

    private NetworkStream stream;
    private volatile bool connected;

    public ProtoGDWorker(IServices server, TcpClient connection)
    {
        this.server = server;
        this.connection = connection;
        try
        {
            stream = connection.GetStream();
            connected = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }

    public virtual void Run()
    {
        while (connected)
        {
            try
            {
                GDRequest request = GDRequest.Parser.ParseDelimitedFrom(stream);
                GDResponse response = handleRequest(request);
                if (response != null)
                {
                    sendResponse(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            try
            {
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        try
        {
            stream.Close();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error " + e);
        }
    }

    private void sendResponse(GDResponse response)
    {
        Console.WriteLine("sending response " + response);
        lock (stream)
        {
            response.WriteDelimitedTo(stream);
            stream.Flush();
        }
    }

    private GDResponse handleRequest(GDRequest request)
    {
        GDResponse response = null;

        try
        {
            switch (request.Type)
            {
                case GDRequest.Types.Type.Login:
                    GestiuneDonatii.model.Voluntar user = ProtoUtils.GetVoluntar(request);
                    server.Login(user, this);
                    response = ProtoUtils.CreateOkResponse();
                    break;
                case GDRequest.Types.Type.GetDonations:
                    var donations = server.GetAllDonatii();
                    response = ProtoUtils.CreateGetDonationsResponse(donations);
                    break;
                case GDRequest.Types.Type.GetDonators:
                    var donatori = server.GetDonatori();
                    response = ProtoUtils.CreateGetDonatorsResponse(donatori);
                    break;
                case GDRequest.Types.Type.NewDonator:
                    GestiuneDonatii.model.Donator don = ProtoUtils.GetDonator(request);
                    server.AddDonator(don);
                    response = ProtoUtils.CreateOkResponse();
                    break;
                case GDRequest.Types.Type.FindDonator:
                    GestiuneDonatii.model.Donator donator = ProtoUtils.GetDonator(request);
                    GestiuneDonatii.model.Donator d = server.FindDonator(donator);
                    response = ProtoUtils.CreateFindDonatorResponse(d);
                    break;
                case GDRequest.Types.Type.NewDonation:
                    GestiuneDonatii.model.Donatie donatie = ProtoUtils.GetDonatie(request);
                    server.AddDonatie(donatie);
                    response = ProtoUtils.CreateOkResponse();
                    break;
                case GDRequest.Types.Type.FindCauza:
                    GestiuneDonatii.model.Cauza cauza = server.FindByNume(ProtoUtils.GetCauza(request));
                    response = ProtoUtils.CreateFindCauzaResponse(cauza);
                    break;
                case GDRequest.Types.Type.Logout:
                    Console.WriteLine("Logout request");
                    GestiuneDonatii.model.Voluntar userLogout = ProtoUtils.GetVoluntar(request);
                    server.Logout(userLogout, this);
                    connected = false;
                    response = ProtoUtils.CreateOkResponse();
                    break;
            }
        }
        catch (ServiceException e)
        {
            response = ProtoUtils.CreateErrorResponse(e.Message);
        }

        if (response == null)
        {
            response = ProtoUtils.CreateErrorResponse("AICI E PROBLEMA");
        }

        return response;
    }
    

    public void NewDonation(GestiuneDonatii.model.Donatie donatie)
    {
        GDResponse resp = ProtoUtils.CreateNewDonationResponse(donatie);
        Console.WriteLine("New donation response " + resp);
        try
        {
            sendResponse(resp);
        }
        catch (IOException e)
        {
            throw new ServiceException("Sending error: " + e);
        }
    }

    public void NewDonator(GestiuneDonatii.model.Donator? donator)
    {
        var resp = ProtoUtils.CreateNewDonatorResponse(donator);
        Console.WriteLine("New donator response " + resp);
        try
        {
            sendResponse(resp);
        }
        catch (IOException e)
        {
            throw new ServiceException("Sending error: " + e);
        }
    }
}