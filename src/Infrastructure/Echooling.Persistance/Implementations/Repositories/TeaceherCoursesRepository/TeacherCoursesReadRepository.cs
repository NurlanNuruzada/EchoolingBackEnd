using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository.TeacherCourses;
using Echooling.Aplication.Abstraction.Repository.TeacherRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Echooling.Persistance.Implementations.Repositories.TeaceherCoursesRepository
{
    public class TeacherCoursesReadRepository : ReadRepository<TeacherDetails_Courses>, ITeacherCoursesReadRepository
    {
        public TeacherCoursesReadRepository(AppDbContext context) : base(context) { }
    }
}

