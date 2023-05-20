namespace TickManager;

public interface IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int HP { get; set; }
}