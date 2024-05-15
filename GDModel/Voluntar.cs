namespace GestiuneDonatii.model;

[Serializable]
public class Voluntar : Entity<long>
{
    public String Username { get; set; }
    public String Email { get; set; }
    public String Parola { get; set; }
    public String Nume { get; set; }
    public long Telefon{ get; set; }

    public Voluntar(long id, string username, string parola) : base(id)
    {
        Username = username;
        Parola = parola;
    }
}