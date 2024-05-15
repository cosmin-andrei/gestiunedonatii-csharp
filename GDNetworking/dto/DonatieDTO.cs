namespace GDNetworking.dto;

public class DonatieDTO
{
    public string Nume { get; set; }
    public float Suma { get; set; }

    public DonatieDTO(string nume, float suma)
    {
        Nume = nume;
        Suma = suma;
    }
}