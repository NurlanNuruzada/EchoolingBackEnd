using System.ComponentModel.DataAnnotations;

namespace Ecooling.Domain.Entites;
public class BaseEntity
{
    [Key]
    public Guid GuId { get; set; }
    public virtual bool IsDeleted { get; set; }
    public DateTime DateModified { get; set; }
    public DateTime DateCreated { get; set; }
}

