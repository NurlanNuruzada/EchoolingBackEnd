using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecooling.Domain.Entites;

namespace Echooling.Aplication.DTOs.CourseDTOs
{
    public class CourseGetDto
    {
        public ICollection<TeacherDetails_Courses>? TeacherDetailsCourses { get; set; }
        public Guid GuId { get; set; }
        public string? ImageRoutue { get; set; }
        public Guid CourseCategoryId { get; set; }
        public string Title { get; set; } = null!;
        public decimal Rate { get; set; }
        public decimal Price { get; set; } 
        public string Instructor { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public string Languge { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public int Enrolled { get; set; }
        public string ThisCourseIncludes { get; set; } = null!;
        public string AboutCourse { get; set; } = null!;
        public string WhatWillLearn { get; set; } = null!;
        public bool Approved { get; }
    }
}
