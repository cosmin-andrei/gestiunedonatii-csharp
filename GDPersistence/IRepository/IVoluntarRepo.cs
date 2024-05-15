using GestiuneDonatii.model;

namespace GestiuneDonatii.Repository;

public interface IVoluntarRepo : IRepository<long, Voluntar>
{
    Voluntar VerifyLogin(string username, string hashPassword);
    Voluntar FindByUsername(string username);
}