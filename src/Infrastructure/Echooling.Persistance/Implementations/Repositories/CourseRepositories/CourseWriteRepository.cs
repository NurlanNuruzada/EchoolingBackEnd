using Echooling.Aplication.Abstraction.Repository.Couse;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.CourseRepositories
{
    public class CourseWriteRepository : WriteRepository<Course>, ICourseWriteRepository
    {
        public CourseWriteRepository(AppDbContext Context) : base(Context)
        {

        }
    }
}
