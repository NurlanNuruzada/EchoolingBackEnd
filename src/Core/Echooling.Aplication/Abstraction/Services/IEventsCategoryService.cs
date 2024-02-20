using Echooling.Aplication.DTOs.CategoryDTOs;

namespace Echooling.Aplication.Abstraction.Services;

public interface IEventsCategoryService
{
    Task Create(EventCategoryDto eventCategoryDto);
    Task<CategoryGetDto> getById(Guid id);
    Task<List<CategoryGetDto>> GetAllAsync();
    Task UpdateAsync(EventCategoryDto eventCategoryDto, Guid id);
    Task Remove(Guid id);
}
