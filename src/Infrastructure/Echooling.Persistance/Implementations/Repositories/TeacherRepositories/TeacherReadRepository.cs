using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository.SliderRepositories;
using Echooling.Aplication.Abstraction.Repository.TeacherRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.TeacherRepositories
{
    public class TeacherReadRepository : ReadRepository<teacherDetails>, ITeacherReadRepository
    {
        public TeacherReadRepository(AppDbContext context) : base(context) { }
    }
}
