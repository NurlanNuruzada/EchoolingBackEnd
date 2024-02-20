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
    public class SliderWriteRepository : WriteRepository<Slider>, ISliderWriteRepository
    {
        public SliderWriteRepository(AppDbContext context) : base(context) { }
    }
}
