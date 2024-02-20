namespace Ecooling.Domain.Entites;
public class EventCategoryies:BaseEntity
{
    public string? Category {  get; set; } 
    public ICollection<events>? Events { get; set; } = new List<events>();
}
