using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.DTOs.CourseReviewDTOs
{
    public class UpdateCourseReviewDto
    {
        public string? Comment { get; set; }
        public decimal rate { get; set; }
        public Guid CourseId { get; set; }
    }
}
