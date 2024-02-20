using Ecooling.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace Ecooling.Domain.Entites
{
    public class AppUser : IdentityUser
    {
        public string? Fullname { get; set; }
        public string? PhoneNumber { get; set; }
        public bool isActive { get; set; }
        public bool? IsSendNewsConfirmed { get; set; }
        public DateTime? RefrestTokenExpiration { get; set; }
        public string? RefrestToken { get; set; }
        public ICollection<Course_AppUser>? CourseAppUser { get; set; }
        public ICollection<AppUser_Events>? AppUserEvents { get; set; }
    }
}
