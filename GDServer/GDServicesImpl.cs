using System.Collections;
using System.Collections.Concurrent;
using GDServices;
using GestiuneDonatii.model;
using GestiuneDonatii.Repository;

namespace GestiuneDonatii.service;

public class GDServicesImpl : IServices
{
    private readonly IDonatorRepo donatorRepo;
    private readonly IDonatieRepo donatieRepo;
    private readonly ICauzaRepo cauzaRepo;
    private readonly IVoluntarRepo voluntarRepo;
    private readonly ConcurrentDictionary<string, IObserver> loggedClients;

    private readonly List<IObserver> observers;

    private const int defaultThreadsNo = 5;

    public GDServicesImpl(IDonatorRepo donatorRepo, IDonatieRepo donatieRepo, ICauzaRepo cauzaRepo,
        IVoluntarRepo voluntarRepo)
    {
        this.donatorRepo = donatorRepo;
        this.donatieRepo = donatieRepo;
        this.cauzaRepo = cauzaRepo;
        this.voluntarRepo = voluntarRepo;
        loggedClients = new ConcurrentDictionary<string, IObserver>();
        observers = new List<IObserver>();
    }

    public void Login(Voluntar voluntar, IObserver client)
    {
        Voluntar userR = voluntarRepo.VerifyLogin(voluntar.Username, voluntar.Parola);
        if (userR == null)
            throw new ServiceException("Invalid username or password.");
        if (loggedClients.ContainsKey(userR.Username))
            throw new ServiceException("User already logged in.");
        loggedClients.TryAdd(userR.Username, client);
        observers.Add(client);
    }

    public void AddDonator(Donator? donator)
    {
        Guid uuid = Guid.NewGuid();
        long id = BitConverter.ToInt64(uuid.ToByteArray(), 0) & long.MaxValue;
        donator.Id = id;
        donatorRepo.save(donator);
        NotifyAllNewDonators(donator);
    }

    private void NotifyAllNewDonators(Donator? donator)
    {
        foreach (var observer in observers)
        {
            Task.Run(() =>
            {
                try
                {
                    observer.NewDonator(donator);
                }
                catch (ServiceException e)
                {
                    Console.Error.WriteLine("Error notifying donator: " + e.Message);
                }
            });
        }
    }

    public List<Donator> GetDonatori()
    {
        IEnumerable<Donator> donators = donatorRepo.findAll();
        return new List<Donator>(donators);
    }

    public Donator? FindDonator(Donator? donator)
    {
        return donatorRepo.findDonator(donator.Nume) ?? null;
    }

    public void AddDonatie(Donatie donatie)
    {
        Guid uuid = Guid.NewGuid();
        long id = BitConverter.ToInt64(uuid.ToByteArray(), 0) & long.MaxValue;
        donatie.Id = id;
        donatieRepo.save(donatie);
        NotifyAllNewDonations(donatie);
    }

    private void NotifyAllNewDonations(Donatie donatie)
    {
        foreach (var observer in observers)
        {
            Task.Run(() =>
            {
                try
                {
                    observer.NewDonation(donatie);
                }
                catch (ServiceException e)
                {
                    Console.Error.WriteLine("Error notifying donator: " + e.Message);
                }
            });
        }
    }


    public Cauza FindByNume(Cauza cauza)
    {
        return cauzaRepo.findByNume(cauza.Nume) ?? null;
    }

    public void Logout(Voluntar voluntar, IObserver client)
    {
        if (!loggedClients.TryRemove(voluntar.Username, out var localClient))
            throw new ServiceException($"User {voluntar.Id} is not logged in.");
    }

    public Dictionary<string, float> GetAllDonatii()
    {
        Dictionary<string, float> donatii = new Dictionary<string, float>();

        foreach (var cauza in cauzaRepo.findAll())
        {
            donatii.Add(cauza.Nume, 0.0f);
        }

        foreach (var donatie in donatieRepo.findAll())
        {
            long id = donatie.Cauza.Id;
            Cauza cauza = cauzaRepo.findOne(id);
            float suma = donatie.Suma;
            donatii[cauza.Nume] += suma;
           
        }

        return donatii;
    }
}