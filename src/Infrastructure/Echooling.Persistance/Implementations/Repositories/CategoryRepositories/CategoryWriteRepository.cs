using Echooling.Aplication.Abstraction.Repository.CourseCategory;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.CategoryRepositories
{
    public class CategoryWriteRepository : WriteRepository<CourseCategories>, ICourseCategoryWriteRepository
    {
        public CategoryWriteRepository(AppDbContext Context) : base(Context)
        {

        }
    }
}
