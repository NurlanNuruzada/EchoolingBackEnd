namespace Echooling.Aplication.Abstraction.Services
{
    public interface IVideoContentCourseService
    {
        Task AddVideoToCourse(Guid CourseId, Guid VideoId);
    }
}
