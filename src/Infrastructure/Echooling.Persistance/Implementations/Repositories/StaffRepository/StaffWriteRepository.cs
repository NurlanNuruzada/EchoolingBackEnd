using Echooling.Aplication.Abstraction.Repository.StaffRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.StaffRepository;

public class StaffWriteRepository : WriteRepository<Staff>, IStaffWriteRepository
{
    public StaffWriteRepository(AppDbContext context) : base(context) { }
}

