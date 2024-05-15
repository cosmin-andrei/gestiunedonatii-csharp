namespace GestiuneDonatii.model;


[Serializable]
public class Donatie : Entity<long>
{
    public Donator? Donator { get; set; }
    public Cauza Cauza { get; set; }
    public String Data { get; set; }
    public float Suma { get; set; }

    public Donatie(long id, Donator? donator, Cauza cauza, float suma) : base(id)
    {
        Donator = donator;
        Cauza = cauza;
        Data = DateTime.Now.ToString();
        Suma = suma;
    }
    
}