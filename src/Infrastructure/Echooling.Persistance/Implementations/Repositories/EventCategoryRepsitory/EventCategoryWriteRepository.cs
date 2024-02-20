using Echooling.Aplication.Abstraction.Repository.EventCategoryRepository;
using Echooling.Aplication.Abstraction.Repository.EventRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.EventCategoryRepsitory
{
    public class EventCategoryWriteRepository : WriteRepository<EventCategoryies>, IEventCategoryWriteRepository
    {
        public EventCategoryWriteRepository(AppDbContext Context) : base(Context)
        {

        }
    }
}
