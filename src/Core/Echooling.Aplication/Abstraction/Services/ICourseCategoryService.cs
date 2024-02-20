using Echooling.Aplication.DTOs.AuthDTOs;
using Echooling.Aplication.DTOs.CategoryDTOs;
using Echooling.Aplication.DTOs.ResponseDTOs;
using Echooling.Aplication.DTOs.SliderDTOs;

namespace Echooling.Aplication.Abstraction.Services;
public interface ICourseCategoryService
{
    Task CreateCourseCategory(CourseCategoryDto courseCategoryDto);
    Task<CategoryGetDto> GetCourseCategoryById(Guid id);
    Task<List<CategoryGetDto>> GetAllAsync();
    Task UpdateAsync(CourseCategoryDto courseCategoryDto, Guid id);
    Task Remove(Guid id);
}
