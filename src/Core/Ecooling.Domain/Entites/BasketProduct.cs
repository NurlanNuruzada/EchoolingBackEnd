namespace Ecooling.Domain.Entites;

public class BasketProduct : BaseEntity
{
    public int Quantity { get; set; }
    public Guid? CourseId { get; set; }
    public Course? Course  { get; set; }
    public Guid? EventsId { get; set; }
    public events? Events { get; set; }
    public Guid BasketId { get; set; }
    public Basket Basket { get; set; }
}
