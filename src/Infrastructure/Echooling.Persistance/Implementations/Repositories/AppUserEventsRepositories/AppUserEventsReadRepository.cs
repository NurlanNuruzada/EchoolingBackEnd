using Echooling.Aplication.Abstraction.Repository.AppUserEventRepository;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.AppUserEventsRepositories
{
    public class AppUserEventsReadRepository:ReadRepository<AppUser_Events>,IAppuserEventReadRopository
    {
        public AppUserEventsReadRepository(AppDbContext context):base(context) { }
    }
}
