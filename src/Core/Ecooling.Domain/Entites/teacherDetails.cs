using System;
using System.Collections.Generic;

namespace Ecooling.Domain.Entites;
public class teacherDetails : BaseEntity
{
    public teacherDetails()
    {
        Role = "Teacher";
    }
    public string Role { get; set; } = "Teacher";
    public Guid? AppUserID { get; set; }
    public ICollection<TeacherDetails_Courses>? TeacherDetailsCourses { get; set; }
    public string? hobbies { get; set; }
    public string? faculty { get; set; }
    public int? TotalExperianceHours { get; set; }
    public int? totalOnlineCourseCount { get; set; }
    public int? totalStudentCount { get; set; }
    public string? Facebook { get; set; }
    public string? twitter { get; set; }
    public string? linkedin { get; set; }
    public string? instagram { get; set; }
    public string? profession {get; set; }
    public string? PhoneNumber { get; set; }
    public string? Fullname { get; set; }
    public string? AboutMe { get; set; }
    public string? emailAddress { get; set; }
    public string? userKnowledge { get; set; }
    public string? UserName { get; set; }
    public bool? IsApproved { get; set; }
    public string? ImageRoutue { get; set; }
}
