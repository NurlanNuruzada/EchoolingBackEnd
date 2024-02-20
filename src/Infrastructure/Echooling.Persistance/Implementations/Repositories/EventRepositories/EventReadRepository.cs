using Echooling.Aplication.Abstraction.Repository.EventRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.EventRepositories
{
    public class EventReadRepository:ReadRepository<events>,IEventReadRepository
    {
        public EventReadRepository(AppDbContext context):base(context)
        {
        }
    }
}
