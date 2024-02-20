namespace Ecooling.Domain.Entites;
public class CourseCategories : BaseEntity
{
    public string? Category { get; set; }
    public ICollection<Course>? Courses { get; set; } = new List<Course>();
}