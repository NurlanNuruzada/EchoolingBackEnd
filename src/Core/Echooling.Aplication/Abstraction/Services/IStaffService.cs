using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.DTOs.SliderDTOs;
using Echooling.Aplication.DTOs.StaffDTOs;

namespace Echooling.Aplication.Abstraction.Services;

public interface IStaffService
{
    Task CreateAsync(CreateStaffDto CreateStaffDto, Guid id);
    Task<GetStaffDto> getById(Guid id);
    Task<List<GetUserListDto>> GetAllAsync();
    Task UpdateAsync(CreateStaffDto StaffUpdateDto, Guid id);
    Task Remove(Guid UserId, Guid AppUserDeletedById);
    Task ApproveStaff(Guid StaffId, Guid ApprovePersonId);
}
