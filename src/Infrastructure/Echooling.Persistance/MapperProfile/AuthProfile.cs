using AutoMapper;
using Echooling.Aplication.DTOs.AuthDTOs;
using Ecooling.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Persistance.MapperProfile
{
    public class AuthProfile:Profile
    {
        public AuthProfile()
        {
         CreateMap<AppUser,RegisterDto>().ReverseMap();   
        }
    }
}
