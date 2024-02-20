using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository.Couse;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.CourseRepositories
{
    public class CourseReadRepository : ReadRepository<Course>, ICourseReadRepository
    {
        public CourseReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
