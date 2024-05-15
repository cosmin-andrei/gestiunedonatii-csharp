using GDNetworking.dto;
using GestiuneDonatii.model;

namespace GDNetworking.jsonprotocol;

public class Request
{
    public RequestType Type { get; set; }
    public UserDTO Voluntar { get; set; }
    public List<Donator> Donatori { get; set; }
    public Donator? Donator { get; set; }
    public Donatie Donatie { get; set; }
    public Cauza Cauza { get; set; }
}