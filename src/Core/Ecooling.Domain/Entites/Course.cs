using System;
using System.Collections.Generic;
using Ecooling.Domain.Entities;
using Microsoft.Extensions.Hosting;

namespace Ecooling.Domain.Entites
{
    public class Course : BaseEntity
    {
        public ICollection<Course_AppUser>? CourseAppUser { get; set; }
        public ICollection<TeacherDetails_Courses>? TeacherDetailsCourses { get; set; }
        public ICollection<CourseReview> CourseReviews { get; } = new List<CourseReview>();
        public ICollection<VideoContent> Videos { get; set; } = new List<VideoContent>();
        public string Title { get; set; } = null!;
        public string ImageRoutue { get; set; } = null!;
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public string Instructor { get; set; } = null!;
        public string? Duration { get; set; }
        public string Languge { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public int Enrolled { get; set; } = 0;
        public string ThisCourseIncludes { get; set; } = null!;
        public string WhatWillLearn { get; set; } = null!;
        public string AboutCourse { get; set; } = null!;
        public Guid CourseCategoryId { get; set; }
        public CourseCategories CourseCategory { get; set; } = null!;    
        public bool Approved { get; set; }
    }
}
