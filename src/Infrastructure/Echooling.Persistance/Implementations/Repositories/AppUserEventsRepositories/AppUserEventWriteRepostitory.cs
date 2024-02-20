using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository;
using Echooling.Aplication.Abstraction.Repository.AppUserEventRepository;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.AppUserEventsRepositories
{
    public class AppUserEventWriteRepostitory:WriteRepository<AppUser_Events>,IAppuserEventWriteRepository
    {
        public AppUserEventWriteRepostitory(AppDbContext context):base(context)
        {
        }
    }
}
