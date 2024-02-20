using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Ecooling.Domain.Entites
{
    public class CourseReview:BaseEntity
    {
        public Guid UserId { get; set; }
        public bool isEdited { get; set; }
        public string? Comment { get; set; }
        public decimal rate { get; set; }
        public string? Fullname { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
 