using Echooling.Aplication.DTOs.EventDTOs;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Http;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface IEventService
    {
        Task CreateAsync(EventCreateDto CreateStaffDto,Guid UsetId);
        Task<EventGetDto> getById(Guid id);
        Task<List<EventGetDto>> getEventsbyStaffId(Guid StaffId);
        Task BuyEvent(Guid courseId, Guid appUserId);
        Task<List<GetBouthEventDto>> GetBouthEvent(Guid appUserId);
        Task<List<EventGetDto>> GetAllAsync();
        Task<List<EventGetDto>> GetAllAsyncTake(int take);
        Task UpdateAsync(EventCreateDto eventUpdate, Guid id);
        Task Remove(Guid id);
        Task<List<EventGetDto>> GetBouthEvents(Guid appUserId);
        Task<List<EventGetDto>> GetAllSearchAsync(string? EventName,
                                                         string? category,
                                                         DateTime? StartDate,
                                                         DateTime? EndDate,
                                                         string? location);
    }
}
