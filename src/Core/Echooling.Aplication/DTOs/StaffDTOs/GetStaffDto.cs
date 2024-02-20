using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.DTOs.StaffDTOs
{
    public record GetStaffDto(string? profession,
                              string? instagram,
                              string? linkedin,
                              string? AboutMe,
                              string? twitter,
                              string? Facebook,
                              int? EventCount,
                              string? LastestEvent,
                              int? TotalExperianceHours,
                              string? faculty,
                              string? hobbies,
                              string? Role,
                              string? PhoneNumber,
                              string? Fullname,
                              Guid GuId,
                              string? emailAddress,
                              string? StartExperiance,
                              string? Follower,
                               string? UserName,
                              Guid AppUserID,
                              bool? IsApproved);
}
