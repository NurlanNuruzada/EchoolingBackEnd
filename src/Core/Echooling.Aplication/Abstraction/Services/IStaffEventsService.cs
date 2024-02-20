using Echooling.Aplication.DTOs.StaffDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface IStaffEventsService
    {
        Task AddStaffToEventAsync(Guid eventId, Guid staffId);
        Task<List<GetStaffDto>> GetByEventOrStaffId(Guid id);
        Task<List<Staff_Events>> GetAllAsync();
        Task UpdateAsync(Staff_Events StaffEvents, Guid id);
    }
}
