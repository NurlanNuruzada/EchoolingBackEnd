using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Repository.VideoRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.VideoContentRepositories
{
    public class VideoContentReadRepository:ReadRepository<VideoContent>,IVideoContentReadRepository
    {
        public VideoContentReadRepository(AppDbContext context):base(context) { }
    }
}
