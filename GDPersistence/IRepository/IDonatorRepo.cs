using GestiuneDonatii.model;

namespace GestiuneDonatii.Repository;

public interface IDonatorRepo : IRepository<long, Donator>
{
    Donator? findDonator(string nume);
    bool isExist(string nume);
}