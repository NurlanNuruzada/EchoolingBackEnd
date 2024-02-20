using System;

namespace Ecooling.Domain.Entites
{
    public class TeacherDetails_Courses : BaseEntity
    {
        public Guid? teacherDetailsId { get; set; }
        public teacherDetails? teacherDetails { get; set; }

        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
