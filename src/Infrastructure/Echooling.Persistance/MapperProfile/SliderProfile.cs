using AutoMapper;
using Echooling.Aplication.DTOs.SliderDTOs;
using Ecooling.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Persistance.MapperProfile
{
    public class SliderProfile : Profile
    {
        public SliderProfile()
        {
            CreateMap<Slider, SliderCreateDto>().ReverseMap();
            CreateMap<Slider, SliderGetDto>().ReverseMap();
            CreateMap<Slider, SliderUpdateDto>().ReverseMap();
            CreateMap<Slider, SliderRemoveDto>().ReverseMap();
        }
    }
}
