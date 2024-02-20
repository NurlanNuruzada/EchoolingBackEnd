using Echooling.Aplication.Abstraction.Repository.SliderRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Persistance.Implementations.Repositories.SliderRepositories
{
    public class SliderReadRepository : ReadRepository<Slider>,ISliderReadRepository
    {
        public SliderReadRepository(AppDbContext context):base(context) { }
    }
}
