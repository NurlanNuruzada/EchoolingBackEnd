using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository.EventRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.EventRepositories
{
    public class EventWriteRepository:WriteRepository<events>,IEventWriteRepository
    {
        public EventWriteRepository(AppDbContext Context):base(Context)
        {
            
        }
    }
}
