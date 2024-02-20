namespace Echooling.Aplication.DTOs.BasketDTOs;

public class BasketProductListDto
{
    public int Quantity { get; set; }
    public Guid BasketId { get; set; }
    public Guid? CourseId { get; set; }
    public Guid? EventId { get; set; }
}
