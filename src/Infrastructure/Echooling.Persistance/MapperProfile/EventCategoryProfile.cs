using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Echooling.Aplication.DTOs.CategoryDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.MapperProfile
{
    public class EventCategoryProfile : Profile
    {
        public EventCategoryProfile()
        {
            CreateMap<EventCategoryies, EventCategoryDto>().ReverseMap();
            CreateMap<EventCategoryies, CategoryGetDto>().ReverseMap();
        }
    }
}
