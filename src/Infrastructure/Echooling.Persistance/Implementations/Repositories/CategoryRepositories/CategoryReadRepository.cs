using Echooling.Aplication.Abstraction.Repository;
using Echooling.Aplication.Abstraction.Repository.CourseCategory;
using Echooling.Aplication.Abstraction.Repository.EventRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.CategoryRepositories;
public class CategoryReadRepository : ReadRepository<CourseCategories>, ICourseCategoryReadRepository
{
    public CategoryReadRepository(AppDbContext context) : base(context)
    {
    }
}
