namespace GDNetworking.dto;


[Serializable]
public class UserDTO
{
    public string Id { get; set; }
    public string Passwd { get; set; }

    public UserDTO(string id, string passwd)
    {
        Id = id;
        Passwd = passwd;
    }

    public override string ToString()
    {
        return $"UserDTO[{Id} {Passwd}]";
    }
}