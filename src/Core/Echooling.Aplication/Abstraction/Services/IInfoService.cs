using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.DTOs.StaffDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface IInfoService
    {
        Task<Info> getInfo();
    }
}
