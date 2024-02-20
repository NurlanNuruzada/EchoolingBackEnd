using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecooling.Domain.Entites;

namespace Echooling.Aplication.DTOs.CourseReviewDTOs
{
    public class CreateCourseReviewDto
    {
        public Guid UserId { get; set; }
        public string? Comment { get; set; }
        public decimal rate { get; set; }
        public string? Fullname { get; set; }
        public Guid CourseId { get; set; }
    }
}
