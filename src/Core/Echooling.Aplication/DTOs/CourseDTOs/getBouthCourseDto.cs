using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecooling.Domain.Entites;

namespace Echooling.Aplication.DTOs.CourseDTOs
{
    public class getBouthCourseDto
    {
        public string Title { get; set; } = null!;
        public Guid GuId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public ICollection<VideoContent> Videos { get; set; } = new List<VideoContent>();
        public CourseCategories CourseCategory { get; set; } = null!;
        public string ImageRoutue { get; set; } = null!;
    }
}
