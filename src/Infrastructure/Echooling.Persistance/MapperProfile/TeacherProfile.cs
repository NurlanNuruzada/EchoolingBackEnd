using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Echooling.Aplication.DTOs.SliderDTOs;
using Echooling.Aplication.DTOs.StaffDTOs;
using Echooling.Aplication.DTOs.TeacherDetailsDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.MapperProfile
{
    public class TeacherProfile:Profile
    {
        public TeacherProfile()
        {
            CreateMap<teacherDetails, TeacherCreateDto>().ReverseMap();
            CreateMap<teacherDetails, TeacherGetDto>().ReverseMap();
            CreateMap<teacherDetails, TeacherUpdateDto>().ReverseMap();
            CreateMap<teacherDetails, GetUserListDto>().ReverseMap();
        }
    }
}
