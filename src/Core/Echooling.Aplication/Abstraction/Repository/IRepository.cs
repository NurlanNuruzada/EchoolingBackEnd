using Ecooling.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Echooling.Aplication.Abstraction.Repository
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public DbSet<T> Table { get; }

    }
}
