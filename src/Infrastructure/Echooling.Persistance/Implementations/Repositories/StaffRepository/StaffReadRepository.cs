using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository.StaffRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.StaffRepository
{
    public class StaffReadRepository : ReadRepository<Staff>, IStaffReadRepository
    {
        public StaffReadRepository(AppDbContext context) : base(context) { }
    }
}
