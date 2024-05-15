using GDNetworking.dto;
using GestiuneDonatii.model;

namespace GDNetworking.jsonprotocol;

[Serializable]
public class Response
{
    public ResponseType Type { get; set; }
    public string ErrorMessage { get; set; }
    public UserDTO Voluntar { get; set; }
    public List<Donator> Donatori { get; set; }
    public Dictionary<string, float> Donatii { get; set; }
    public Donatie Donatie { get; set; }
    public Donator? Donator { get; set; }
    public Cauza Cauza { get; set; }
}