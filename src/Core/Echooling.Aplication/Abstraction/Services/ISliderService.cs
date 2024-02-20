using Echooling.Aplication.DTOs.SliderDTOs;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface ISliderService
    {
        Task CreateAsync(SliderCreateDto categoryCreateDto);
        Task<SliderGetDto> getById(Guid id);
        Task<List<SliderGetDto>> GetAllAsync();
        Task UpdateAsync(SliderUpdateDto categoryUpdateDto, Guid id);
        Task Remove(Guid id);
    }
}
