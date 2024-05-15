using GDNetworking.dto;
using GestiuneDonatii.model;

namespace GDNetworking.jsonprotocol;

public class JsonProtocolUtils
{
    public static Request CreateLoginRequest(Voluntar user)
    {
        Request req = new Request();
        Console.WriteLine("Create login request");
        req.Type = RequestType.LOGIN;
        req.Voluntar = DTOUtils.GetDTO(user);
        return req;
    }

    public static Response CreateErrorResponse(string errorMessage)
    {
        Response resp = new Response
        {
            Type = ResponseType.ERROR,
            ErrorMessage = errorMessage
        };
        return resp;
    }

    public static Response CreateOkResponse()
    {
        Response resp = new Response
        {
            Type = ResponseType.OK
        };
        return resp;
    }

    public static Request CreateLogoutRequest(Voluntar voluntar)
    {
        Request req = new Request
        {
            Type = RequestType.LOGOUT,
            Voluntar = DTOUtils.GetDTO(voluntar)
        };
        return req;
    }

    public static Request CreateDonatiiRequest()
    {
        return new Request { Type = RequestType.GET_DONATIONS };
    }

    public static Response CreateGetDonationsResponse(Dictionary<string, float> donations)
    {
        Response resp = new Response
        {
            Type = ResponseType.GET_DONATIONS,
            Donatii = donations
        };
        return resp;
    }

    public static Request CreateGetDonatoriRequest()
    {
        return new Request { Type = RequestType.GET_DONATORI };
    }

    public static Response CreateGetDonatoriResponse(List<Donator> donatori)
    {
        Response resp = new Response
        {
            Type = ResponseType.GET_DONATIONS,
            Donatori = donatori
        };
        return resp;
    }

    public static Request CreateAddDonatorRequest(Donator? donator)
    {
        Request req = new Request
        {
            Type = RequestType.NEW_DONATOR,
            Donator = donator
        };
        return req;
    }

    public static Response CreateNewDonationResponse(Donatie donatie)
    {
        Response resp = new Response
        {
            Type = ResponseType.NEW_DONATION,
            Donatie = donatie
        };
        return resp;
    }

    public static Response CreateNewDonatorResponse(Donator? donator)
    {
        Response resp = new Response
        {
            Type = ResponseType.NEW_DONATOR,
            Donator = donator
        };
        return resp;
    }

    public static Request CreateFindDonatorRequest(Donator? donator)
    {
        Request req = new Request
        {
            Type = RequestType.FIND_DONATOR,
            Donator = donator
        };
        return req;
    }

    public static Response CreateFindDonatorResponse(Donator? donator)
    {
        Response resp = new Response
        {
            Type = ResponseType.FIND_DONATOR,
            Donator = donator
        };
        return resp;
    }

    public static Request CreateNewDonationRequest(Donatie donatie)
    {
        Request req = new Request
        {
            Type = RequestType.NEW_DONATION,
            Donatie = donatie
        };
        return req;
    }

    public static Request CreateFindCauzaRequest(Cauza cauza)
    {
        Request req = new Request
        {
            Type = RequestType.FIND_CAUZA,
            Cauza = cauza
        };
        return req;
    }

    public static Response CreateFindCauzaResponse(Cauza cauza)
    {
        Response resp = new Response
        {
            Type = ResponseType.FIND_CAUZA,
            Cauza = cauza
        };
        return resp;
    }
}