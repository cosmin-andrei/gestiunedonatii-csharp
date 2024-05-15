using GestiuneDonatii.model;

namespace GestiuneDonatii.Repository;

public interface ICauzaRepo : IRepository<long, Cauza>
{
    Cauza findByNume(string nume);
}