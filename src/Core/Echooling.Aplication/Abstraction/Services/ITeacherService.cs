using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.DTOs.SliderDTOs;
using Echooling.Aplication.DTOs.TeacherDetailsDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface  ITeacherService
    {
        Task CreateAsync(TeacherCreateDto teacherCreateDto,Guid id);
        Task<List<TeacherGetDto>> GetAllAsync();
        Task<TeacherGetDto> getById(Guid id);
        Task UpdateAsync(TeacherUpdateDto teacherUpdateDto, Guid id);
        Task Remove(Guid UserId, Guid AppUserDeletedById);
        Task ApproveTeacher(Guid teacherId, Guid ApprovePersonId);
    }
}
