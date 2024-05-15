using GDNetworking.dto;
using GDServices;
using GestiuneDonatii;
using GestiuneDonatii.model;
using Microsoft.VisualBasic.ApplicationServices;

namespace GDClient;

public class UserController : IObserver
{
    public event EventHandler<UserEventArgs> updateEvent;  // public event EventHandler<ChatUserEventArgs> updateEvent; //ctrl calls it when it has received an update
    private readonly IServices server;
    private Voluntar currentUser;

    public UserController(IServices server)
    {
        this.server = server;
        currentUser = null;
        
    }

    public void login(String user, String pass)
    {
        Voluntar voluntar = new Voluntar(0, user, pass);
        server.Login(voluntar, this);
        Console.WriteLine("Login succeeded ....");
        currentUser = voluntar;
        Console.WriteLine("Current user {0}", voluntar);
    }


    public void logout()
    {
        Console.WriteLine("Ctrl logout");
        server.Logout(currentUser, this);
        currentUser = null;
        
    }

    public Cauza findCauza(String nume)
    {
        return server.FindByNume(new Cauza(0, nume, ""));
    }
    
    
    protected virtual void OnUserEvent(UserEventArgs e)
    {
        if (updateEvent == null) return;
        updateEvent(this, e);
        Console.WriteLine("Update Event called");
    }

    public void NewDonation(Donatie donatie)
    {
        UserEventArgs userArgs = new UserEventArgs(UserEvent.newDonatie, donatie);
        OnUserEvent(userArgs);
    }

    public void NewDonator(Donator? donator)
    {
        
    }

    public Dictionary<string, float> getDonatii()
    {
         return server.GetAllDonatii();
    }

    public List<Donator>? getDonatori()
    {
        return server.GetDonatori();
    }

    public void addDonatie(string nume, long telefon, string adresa, Cauza cauza, float suma)
    {
        
        Donator? donator = new Donator(0, nume, telefon, adresa);

        if (server.FindDonator(donator) != null)
        {
            donator = server.FindDonator(donator);
            Console.WriteLine("Donator gasit");
        }
        else
        {
            server.AddDonator(donator);
            Console.WriteLine("Donator nou adaugat");
        }

        server.AddDonatie(new Donatie(0, donator, cauza, suma));
        Console.WriteLine("Donatie adaugata");
    }
}