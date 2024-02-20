using Echooling.Aplication.DTOs.CourseDTOs;
using Echooling.Aplication.DTOs.TeacherDetailsDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface ICourseService
    {
        Task<List<getBouthCourseDto>> GetBouthCourses(Guid appUserId);
        Task CreateAsync(CourseCreateDto courseCreateDto, Guid TeacherId);
        Task<CourseGetDto> getById(Guid CourseId);
        Task<List<CourseGetDto>> GetAllAsync();
        Task<List<CourseGetDto>> GetAllSearchAsync(string? courseName, string? category,decimal? rating);
        Task<List<TeacherGetDto>> GetTeachersByCourseId(Guid courseId);
        Task UpdateAsync(CourseCreateDto courseCreateDto, Guid CourseId);
        Task<List<CourseGetDto>> GetLatestWithCategory(int take, Guid? categoryId);
        Task Remove(Guid CourseId);
        Task<List<CourseGetDto>> GetCoursesByTeacherIdAsync(Guid teacherId);
        Task BuyCourse(Guid courseId, Guid appUserId);
    }
}
