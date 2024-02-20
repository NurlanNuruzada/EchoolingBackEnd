using Microsoft.AspNetCore.Http;

public class CourseCreateDto
{
    public IFormFile? image { get; set; }
    public Guid CourseCategoryId { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public string Instructor { get; set; } = null!;
    public string Languge { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string[] ThisCourseIncludes { get; set; } = new string[0];
    public string AboutCourse { get; set; } = null!;
    public string[] WhatWillLearn { get; set; } = new string[0];
    public bool Approved { get; set; }
}