using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Echooling.Aplication.DTOs.VideoDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.MapperProfile
{
    public class VideoContentProfile:Profile
    {
        public VideoContentProfile()
        {
            CreateMap<VideoContent,GetVideoContentDto>().ReverseMap();
            CreateMap<VideoContent,CreateVIdeoContentDto>().ReverseMap();
        }
    }
}
