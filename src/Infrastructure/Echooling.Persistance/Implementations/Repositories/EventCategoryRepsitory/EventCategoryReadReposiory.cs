using Echooling.Aplication.Abstraction.Repository.EventCategoryRepository;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.EventCategoryRepsitory
{
    public class EventCategoryReadReposiory : ReadRepository<EventCategoryies>, IEventCategoryReadRepository
    {
        public EventCategoryReadReposiory(AppDbContext context) : base(context)
        {
        }
    }
}
