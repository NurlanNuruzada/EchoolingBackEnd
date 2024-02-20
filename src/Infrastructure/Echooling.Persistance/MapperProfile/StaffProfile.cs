using AutoMapper;
using Echooling.Aplication.DTOs.StaffDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.MapperProfile;

public class StaffProfile:Profile
{
    public StaffProfile()
    {
        CreateMap<Staff , CreateStaffDto>().ReverseMap();   
        CreateMap<Staff , GetStaffDto>().ReverseMap();   
        CreateMap<Staff, GetUserListDto>().ReverseMap();   
    }
}
