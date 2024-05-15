using GestiuneDonatii.model;

namespace GDServices;

public interface IObserver
{
    void NewDonation(Donatie donatie);
    void NewDonator(Donator? donator);
}