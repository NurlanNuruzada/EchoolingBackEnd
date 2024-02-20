using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.DTOs.StaffDTOs
{
    public class CreateStaffDto
    {
        public string? hobbies { get; set; }
        public string? faculty { get; set; }
        public int? TotalExperianceHours { get; set; }
        public string? LastestEvent { get; set; }
        public int? EventCount { get; set; }
        public string? Facebook { get; set; }
        public string? twitter { get; set; }
        public string? linkedin { get; set; }
        public string? instagram { get; set; }
        public string? profession { get; set; }
        public string? AboutMe { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Fullname { get; set; }
        public string? emailAddress { get; set; }
        public string? UserName { get; set; }
        public bool? IsApproved { get; set; }
        public string? StartExperiance { get; set; }
        public string? Follower { get; set; }
    }
}
