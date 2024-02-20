using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Echooling.Aplication.DTOs.AuthDTOs;
using Echooling.Aplication.DTOs.CategoryDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.MapperProfile
{
    public class CourseCategoryProfile : Profile
    {
        public CourseCategoryProfile()
        {
            CreateMap<CourseCategories, CourseCategoryDto>().ReverseMap();
            CreateMap<CourseCategories, CategoryGetDto>().ReverseMap();

        }
    }
}
