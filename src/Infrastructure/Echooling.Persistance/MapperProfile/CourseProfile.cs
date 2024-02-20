using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Echooling.Aplication.DTOs.CourseDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.MapperProfile
{
    public class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<Course,CourseCreateDto>().ReverseMap();
            CreateMap<Course,CourseGetDto>().ReverseMap();
            CreateMap<Course,getBouthCourseDto>().ReverseMap();
        }
    }
}
