using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository;
using Echooling.Aplication.Abstraction.Repository.TeacherCourses;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.TeaceherCoursesRepository
{
    public class TeacherCourseWriteRepository:WriteRepository<TeacherDetails_Courses>,ITeacherCoursesWriteRepository
    {
        public TeacherCourseWriteRepository(AppDbContext context):base(context) { } 
    }
}
