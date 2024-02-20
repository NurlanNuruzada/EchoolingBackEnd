using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.DTOs;
using Echooling.Aplication.DTOs.SliderDTOs;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface IAppUserEventService
    {
        Task CreateAsync(AppUserEventDto categoryCreateDto);
    }
}
