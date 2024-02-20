namespace Ecooling.Domain.Entites
{
    public class Staff:BaseEntity
    {
        public Staff()
        {
            Role = "Staff";
        }
        public string Role { get; set; } = "Staff";
        public Guid AppUserID { get; set; }
        public ICollection<Staff_Events>? StaffEvents { get; set; }
        public string? hobbies { get; set; }
        public string? faculty { get; set; }
        public string? Facebook { get; set; }
        public string? twitter { get; set; }
        public string? linkedin { get; set; }
        public string? instagram { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Fullname { get; set; }
        public string? profession { get; set; }
        public string? AboutMe { get; set; }
        public string? emailAddress { get; set; }
        public int? TotalExperianceHours { get; set; }
        public string? LastestEvent { get; set; }
        public int? EventCount { get; set; }
        public string? StartExperiance { get; set; }
        public string? Follower { get; set; }
        public string? UserName { get; set; }
        public bool? IsApproved { get; set; }
    }
}
