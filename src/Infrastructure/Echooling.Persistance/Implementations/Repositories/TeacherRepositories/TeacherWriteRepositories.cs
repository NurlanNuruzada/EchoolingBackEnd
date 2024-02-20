using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository.TeacherRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.TeacherRepositories
{
    public class TeacherWriteRepositories: WriteRepository<teacherDetails>,ITeacherWriteRepository
    {
        public TeacherWriteRepositories(AppDbContext appContext):base(appContext) { }
    }
}
