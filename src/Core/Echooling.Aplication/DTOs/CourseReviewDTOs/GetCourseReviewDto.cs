using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.DTOs.CourseReviewDTOs
{
    public class GetCourseReviewDto
    {
        public Guid GuId { get; set; }
        public Guid UserId { get; set; }
        public string? Comment { get; set; }
        public decimal rate { get; set; }
        public string? Fullname { get; set; }
        public Guid CourseId { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
