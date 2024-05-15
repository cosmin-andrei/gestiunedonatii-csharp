using System.Net.Sockets;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using GDNetworking.dto;
using GDServices;
using GestiuneDonatii.model;
using Newtonsoft.Json;

namespace GDNetworking.jsonprotocol;

public class GDClientJsonWorker : IObserver
{
    private IServices server;
    private TcpClient connection;

    private NetworkStream networkStream;
    private StreamReader input;
    private StreamWriter output;
    private JsonSerializer jsonFormatter;
    private volatile bool connected;

    public GDClientJsonWorker(IServices server, TcpClient connection)
    {
        this.server = server;
        this.connection = connection;
        jsonFormatter = new JsonSerializer();
        networkStream = connection.GetStream();
        output = new StreamWriter(networkStream, System.Text.Encoding.UTF8);
        input = new StreamReader(networkStream, System.Text.Encoding.UTF8);
        connected = true;
        Console.WriteLine("GDClientJsonWorker");
    }

    public void Run()
    {
        while (connected)
        {
            try
            {
                Console.WriteLine("Start reading request...");
                string requestLine = input.ReadLine();
                Console.WriteLine("I read: " + requestLine);

                Request request = JsonConvert.DeserializeObject<Request>(requestLine);
                Response response = HandleRequest(request);
                SendResponse(response);
                
            }
            catch (IOException e)
            {
                Console.WriteLine("Error " + e);
            }

            try
            {
                System.Threading.Thread.Sleep(1000);
            }
            catch (IOException e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        try
        {
            input.Close();
            output.Close();
            connection.Close();
        }
        catch (IOException e)
        {
            Console.WriteLine("Error " + e);
        }
    }

    private static Response okResponse = JsonProtocolUtils.CreateOkResponse();

    private Response HandleRequest(Request request)
    {
        Response response = null;

        try
        {
            switch (request.Type)
            {
                case RequestType.LOGIN:
                    UserDTO user = request.Voluntar;
                    Voluntar voluntar = DTOUtils.GetFromDTO(user);
                    server.Login(voluntar, this);
                    response = okResponse;
                    break;
                case RequestType.GET_DONATIONS:
                    Dictionary<string, float> donations = server.GetAllDonatii();
                    response = JsonProtocolUtils.CreateGetDonationsResponse(donations);
                    break;
                case RequestType.GET_DONATORI:
                    List<Donator> donatori = server.GetDonatori();
                    response = JsonProtocolUtils.CreateGetDonatoriResponse(donatori);
                    break;
                case RequestType.NEW_DONATOR:
                    server.AddDonator(request.Donator);
                    Console.WriteLine("New donator response " + request.Donator);
                    // newDonator(request.getDonator());
                    response = okResponse;
                    break;
                case RequestType.FIND_DONATOR:
                    Donator donator = server.FindDonator(request.Donator);
                    response = JsonProtocolUtils.CreateFindDonatorResponse(donator);
                    break;
                case RequestType.NEW_DONATION:
                    server.AddDonatie(request.Donatie);
                    // newDonation(request.getDonatie());
                    response = okResponse;
                    break;
                case RequestType.FIND_CAUZA:
                    Cauza cauza = server.FindByNume(request.Cauza);
                    response = JsonProtocolUtils.CreateFindCauzaResponse(cauza);
                    break;
                case RequestType.LOGOUT:
                    UserDTO logoutUser = request.Voluntar;
                    Voluntar logoutVoluntar = DTOUtils.GetFromDTO(logoutUser);
                    server.Logout(logoutVoluntar, this);
                    connected = false;
                    response = okResponse;
                    break;
            }
        }
        catch (ServiceException e)
        {
            response = JsonProtocolUtils.CreateErrorResponse(e.Message);
        }

        if (response == null)
        {
            response = JsonProtocolUtils.CreateErrorResponse("AICI E PROBLEMA");
            
        }
        return response;
    }

    private void SendResponse(Response response)
    {
        string responseLine = JsonConvert.SerializeObject(response);
        Console.WriteLine("sending response " + responseLine);
        lock (output)
        {
            output.WriteLine(responseLine);
            output.Flush();
        }
    }

    public void NewDonation(Donatie donatie)
    {
        Response resp = JsonProtocolUtils.CreateNewDonationResponse(donatie);
        Console.WriteLine("New donation response " + resp);
        try
        {
            SendResponse(resp);
        }
        catch (IOException e)
        {
            throw new ServiceException("Sending error: " + e);
        }
    }

    public void NewDonator(Donator donator)
    {
        Response resp = JsonProtocolUtils.CreateNewDonatorResponse(donator);
        Console.WriteLine("New donator response " + resp);
        try
        {
            SendResponse(resp);
        }
        catch (IOException e)
        {
            throw new ServiceException("Sending error: " + e);
        }
    }
}