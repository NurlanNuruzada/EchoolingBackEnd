using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ecooling.Domain.Entites;

namespace Echooling.Aplication.DTOs.TeacherDetailsDTOs
{
    public class TeacherCreateDto
    {
        public string? hobbies { get; set; }
        public string? faculty { get; set; }
        public int? TotalExperianceHours { get; set; }
        public int? totalOnlineCourseCount { get; set; }
        public int? totalStudentCount { get; set; }
        public string? Facebook { get; set; }
        public string? twitter { get; set; }
        public string? linkedin { get; set; }
        public string? instagram { get; set; }
        public string? profession { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Fullname { get; set; }
        public string? AboutMe { get; set; }
        public string? userKnowledge { get; set; }
        public string? UserName { get; set; }
        public bool? IsApproved { get; set; }
    }
}
