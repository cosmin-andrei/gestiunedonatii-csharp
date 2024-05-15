using GestiuneDonatii.model;

namespace GDServices;

public interface IServices
{
    void Login(Voluntar voluntar, IObserver client);
    void AddDonator(Donator? donator);
    List<Donator> GetDonatori();
    Donator? FindDonator(Donator? donator);
    void AddDonatie(Donatie donatie);
    Cauza FindByNume(Cauza cauza);
    void Logout(Voluntar voluntar, IObserver client);
    Dictionary<string, float> GetAllDonatii();
}