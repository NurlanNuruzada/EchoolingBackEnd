using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository.EventsStaff;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.StaffEventsRepositories
{
    public class StaffEventsReadRepository:ReadRepository<Staff_Events>,IEventStaffReadRepository 
    {
        public StaffEventsReadRepository(AppDbContext context):base(context) { }
    }
}
