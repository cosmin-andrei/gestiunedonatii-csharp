namespace GestiuneDonatii.model;


[Serializable]
public class Entity<ID>
{
    public ID Id { get; set; }

    public Entity(ID id)
    {
        this.Id = id;
    }
}