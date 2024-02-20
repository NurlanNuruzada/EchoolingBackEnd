using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository;
using Echooling.Aplication.Abstraction.Repository.TeacherRepositories;
using Echooling.Aplication.Abstraction.Repository.VideoRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.VideoContentRepositories
{
    public class VideoContentWriteRepository : WriteRepository<VideoContent>,IVideoContentWriteRepository
    {
        public VideoContentWriteRepository(AppDbContext appContext) : base(appContext) { }
    }
}
