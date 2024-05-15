namespace GestiuneDonatii.model;

[Serializable]
public class Cauza : Entity<long>
{
    public String Nume { get; set; }
    public String Descriere { get; set; }
    
    public Cauza(long id, string nume, string descriere) : base(id)
    {
        this.Nume = nume;
        this.Descriere = descriere;
    }
}