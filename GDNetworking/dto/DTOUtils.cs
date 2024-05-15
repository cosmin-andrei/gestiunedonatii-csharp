using GestiuneDonatii.model;

namespace GDNetworking.dto;


public static class DTOUtils
{
    public static Voluntar GetFromDTO(UserDTO usdto)
    {
        string id = usdto.Id;
        string pass = usdto.Passwd;
        return new Voluntar(0,id, pass);
    }

    public static UserDTO GetDTO(Voluntar user)
    {
        string id = user.Username;
        string pass = user.Parola;
        return new UserDTO(id, pass);
    }

    public static Dictionary<string, float> GetFromDTODonatii(DonatieDTO[] donatiiDTO)
    {
        var donatii = new Dictionary<string, float>();
        foreach (var donatieDTO in donatiiDTO)
        {
            donatii.Add(donatieDTO.Nume, donatieDTO.Suma);
        }
        return donatii;
    }
}