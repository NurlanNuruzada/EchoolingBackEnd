using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository.CourseReviewRepositories;
using Echooling.Aplication.Abstraction.Repository.Couse;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.CourseReviewRepositories
{
    public class CourseReviewReadRepository:ReadRepository<CourseReview>,ICourseReviewReadRepository
    {
        public CourseReviewReadRepository(AppDbContext context):base(context) { }
    }
}
