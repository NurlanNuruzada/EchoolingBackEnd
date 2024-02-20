using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Echooling.Aplication.DTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.MapperProfile
{
    public class LoggerProfile:Profile
    {
        public LoggerProfile()
        {
            CreateMap<Logger,CreateLogDto>().ReverseMap();
        }
    }
}
