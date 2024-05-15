namespace GestiuneDonatii.model;


[Serializable]
public class Donator : Entity<long>
{
    public String Adresa { get; set; }
    public long Telefon { get; set; }
    public String Nume { get; set; }

    public Donator(long id, string nume, long telefon, string adresa) : base(id)
    {
        Nume = nume;
        Telefon = telefon;
        Adresa = adresa;
    }
    
}